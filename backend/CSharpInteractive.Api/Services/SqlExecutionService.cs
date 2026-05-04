using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace CSharpInteractive.Api.Services;

public sealed record SqlQueryResult(
    bool Success,
    string Output,
    IReadOnlyList<string> Diagnostics,
    long DurationMs,
    IReadOnlyList<string> Columns,
    IReadOnlyList<IReadOnlyDictionary<string, object>> Rows);

public sealed class SqlExecutionService(SqlSafetyService safetyService, IOptions<SqlLearningOptions> options)
{
    private static readonly Regex SelectRegex = new(
        @"^\s*SELECT\s+(?:TOP\s+(?<top>\d+)\s+)?(?:(?<distinct>DISTINCT)\s+)?(?<columns>.+?)\s+FROM\s+(?<table>\w+)(?:\s+WHERE\s+(?<where>.*?))?(?:\s+GROUP\s+BY\s+(?<group>\w+))?(?:\s+HAVING\s+(?<having>.*?))?(?:\s+ORDER\s+BY\s+(?<order>\w+)(?:\s+(?<direction>ASC|DESC))?)?\s*;?\s*$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex JoinRegex = new(
        @"^\s*SELECT\s+(?:TOP\s+(?<top>\d+)\s+)?(?<columns>.+?)\s+FROM\s+Products\s+(?<productAlias>\w+)\s+(?<join>INNER|LEFT|RIGHT|FULL\s+OUTER)\s+JOIN\s+Categories\s+(?<categoryAlias>\w+)\s+ON\s+(?<on>.+?)(?:\s+WHERE\s+(?<where>.*?))?(?:\s+ORDER\s+BY\s+(?<order>(?:\w+\.)?\w+)(?:\s+(?<direction>ASC|DESC))?)?\s*;?\s*$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public async Task<SqlQueryResult> ExecuteQueryAsync(string query)
    {
        var stopwatch = Stopwatch.StartNew();
        var diagnostics = safetyService.ValidateReadOnlyModuleQuery(query);
        if (diagnostics.Count > 0)
        {
            stopwatch.Stop();
            return new SqlQueryResult(false, "", diagnostics, stopwatch.ElapsedMilliseconds, [], []);
        }

        if (options.Value.UseSqlServer && Regex.IsMatch(query, @"^\s*(SELECT|CREATE\s+(TABLE|INDEX|VIEW|PROCEDURE|FUNCTION)|DECLARE|EXEC|INSERT|UPDATE|DELETE|BEGIN\s+TRAN|BEGIN\s+TRANSACTION)", RegexOptions.IgnoreCase))
        {
            return await ExecuteWithSqlServerAsync(query, stopwatch);
        }

        var joinMatch = JoinRegex.Match(query);
        if (joinMatch.Success)
        {
            var joinResult = ExecuteJoinQuery(joinMatch);
            stopwatch.Stop();
            return joinResult with { DurationMs = stopwatch.ElapsedMilliseconds };
        }

        var match = SelectRegex.Match(query);
        if (!match.Success)
        {
            stopwatch.Stop();
            return new SqlQueryResult(false, "", ["Seules les requetes SELECT simples sont autorisees dans ce module."], stopwatch.ElapsedMilliseconds, [], []);
        }

        var tableName = match.Groups["table"].Value;
        var table = GetTable(tableName);
        if (table is null)
        {
            stopwatch.Stop();
            return new SqlQueryResult(false, "", [$"Table inconnue: {tableName}"], stopwatch.ElapsedMilliseconds, [], []);
        }

        var selectedExpression = match.Groups["columns"].Value;
        var where = match.Groups["where"].Success ? match.Groups["where"].Value : "";
        var filteredRows = table.Rows.Where(row => MatchesWhere(row, where)).ToList();

        if (IsAggregateQuery(selectedExpression, match.Groups["group"].Value))
        {
            var aggregateResult = ExecuteAggregateQuery(
                selectedExpression,
                filteredRows,
                match.Groups["group"].Value,
                match.Groups["having"].Value,
                match.Groups["order"].Value,
                match.Groups["direction"].Value,
                match.Groups["top"].Value);

            stopwatch.Stop();
            return aggregateResult with { DurationMs = stopwatch.ElapsedMilliseconds };
        }

        var selectedColumns = ResolveColumns(selectedExpression, table.Columns);
        if (selectedColumns.Count == 0)
        {
            stopwatch.Stop();
            return new SqlQueryResult(false, "", ["Aucune colonne valide selectionnee."], stopwatch.ElapsedMilliseconds, [], []);
        }

        var orderedRows = ApplyOrderBy(filteredRows, match.Groups["order"].Value, match.Groups["direction"].Value);
        var distinctRows = match.Groups["distinct"].Success ? ApplyDistinct(orderedRows, selectedColumns) : orderedRows;
        var limitedRows = ApplyTop(distinctRows, match.Groups["top"].Value);
        stopwatch.Stop();

        return new SqlQueryResult(true, FormatRows(selectedColumns, limitedRows), [], stopwatch.ElapsedMilliseconds, selectedColumns, limitedRows);
    }

    public async Task<ExecutionResultDto> ExecuteAsync(string query)
    {
        var result = await ExecuteQueryAsync(query);
        return new ExecutionResultDto(result.Success, result.Output, result.Diagnostics, result.DurationMs);
    }

    public SqlSchemaDto GetSchema() =>
        new(
            "StoreBasics",
            [
                new SqlTableSchemaDto(
                    "Categories",
                    "Familles de produits du catalogue.",
                    [
                        new SqlColumnSchemaDto("Id", "int", false, "Identifiant unique de la categorie."),
                        new SqlColumnSchemaDto("Name", "nvarchar(80)", false, "Nom affiche de la categorie.")
                    ]),
                new SqlTableSchemaDto(
                    "Products",
                    "Produits disponibles ou archives dans la boutique.",
                    [
                        new SqlColumnSchemaDto("Id", "int", false, "Identifiant unique du produit."),
                        new SqlColumnSchemaDto("Name", "nvarchar(120)", false, "Nom affiche du produit."),
                        new SqlColumnSchemaDto("CategoryId", "int", false, "Categorie associee au produit."),
                        new SqlColumnSchemaDto("Price", "decimal(10,2)", false, "Prix public du produit."),
                        new SqlColumnSchemaDto("Stock", "int", false, "Quantite disponible."),
                        new SqlColumnSchemaDto("IsActive", "bit", false, "1 si le produit est vendable, 0 sinon."),
                        new SqlColumnSchemaDto("DiscontinuedAt", "nvarchar(20)", true, "Date d'archivage si le produit n'est plus vendu.")
                    ])
            ],
            [
                "Une seule instruction par execution.",
                "Lecture seule pour les modules SQL deja disponibles.",
                "Acces aux tables systeme bloque.",
                "Commandes destructives bloquees.",
                options.Value.UseSqlServer ? "Execution sur SQL Server Docker." : "Execution sur moteur pedagogique en memoire."
            ]);

    private async Task<SqlQueryResult> ExecuteWithSqlServerAsync(string query, Stopwatch stopwatch)
    {
        if (string.IsNullOrWhiteSpace(options.Value.ConnectionString))
        {
            stopwatch.Stop();
            return new SqlQueryResult(false, "", ["La connexion SQL Server pedagogique n'est pas configuree."], stopwatch.ElapsedMilliseconds, [], []);
        }

        try
        {
            await using var connection = new SqlConnection(options.Value.ConnectionString);
            await connection.OpenAsync();
            await ResetStoreBasicsAsync(connection, query);

            var columns = new List<string>();
            var rows = new List<IReadOnlyDictionary<string, object>>();
            foreach (var statement in SplitSqlStatements(query))
            {
                await using var command = new SqlCommand(statement, connection)
                {
                    CommandTimeout = 5
                };

                await using var reader = await command.ExecuteReaderAsync();
                while (reader.FieldCount == 0 && await reader.NextResultAsync())
                {
                    // Skip DDL/DML statements until a result set is available.
                }

                if (reader.FieldCount == 0)
                {
                    continue;
                }

                columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                rows.Clear();

                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    foreach (var index in Enumerable.Range(0, reader.FieldCount))
                    {
                        var value = await reader.IsDBNullAsync(index) ? "" : reader.GetValue(index);
                        row[reader.GetName(index)] = value;
                    }

                    rows.Add(row);
                }
            }

            stopwatch.Stop();
            return new SqlQueryResult(true, FormatRows(columns, rows), [], stopwatch.ElapsedMilliseconds, columns, rows);
        }
        catch (SqlException exception)
        {
            stopwatch.Stop();
            return new SqlQueryResult(false, "", [exception.Message], stopwatch.ElapsedMilliseconds, [], []);
        }
    }

    private static async Task ResetStoreBasicsAsync(SqlConnection connection, string query)
    {
        var createsOwnProductsTable = Regex.IsMatch(query, @"\bCREATE\s+TABLE\s+(dbo\.)?Products\b", RegexOptions.IgnoreCase);
        var commands = new List<string>
        {
            "DROP PROCEDURE IF EXISTS dbo.GetActiveProducts;",
            "DROP FUNCTION IF EXISTS dbo.ApplyTax;",
            "DROP VIEW IF EXISTS dbo.ActiveProducts;",
            "DROP TABLE IF EXISTS dbo.OrderItems;",
            "DROP TABLE IF EXISTS dbo.Orders;",
            "DROP TABLE IF EXISTS dbo.Customers;",
            "DROP TABLE IF EXISTS dbo.StudentCourses;",
            "DROP TABLE IF EXISTS dbo.Students;",
            "DROP TABLE IF EXISTS dbo.Courses;",
            "DROP TABLE IF EXISTS dbo.Warehouses;",
            "DROP TABLE IF EXISTS dbo.SupplierProducts;",
            "DROP TABLE IF EXISTS dbo.Suppliers;",
            "DROP TABLE IF EXISTS dbo.Products;",
            "DROP TABLE IF EXISTS dbo.Categories;"
        };

        if (!createsOwnProductsTable)
        {
            commands.AddRange(new[]
            {
            """
            CREATE TABLE dbo.Categories (
                Id int NOT NULL PRIMARY KEY,
                Name nvarchar(80) NOT NULL
            );
            """,
            """
            CREATE TABLE dbo.Products (
                Id int NOT NULL PRIMARY KEY,
                Name nvarchar(120) NOT NULL,
                CategoryId int NOT NULL,
                Price decimal(10,2) NOT NULL,
                Stock int NOT NULL,
                IsActive bit NOT NULL,
                DiscontinuedAt nvarchar(20) NULL,
                CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryId) REFERENCES dbo.Categories(Id)
            );
            """,
            """
            INSERT INTO dbo.Categories (Id, Name)
            VALUES
                (1, N'Books'),
                (2, N'Games'),
                (3, N'Hardware');
            """
            });
        }

        foreach (var commandText in commands)
        {
            await using var command = new SqlCommand(commandText, connection)
            {
                CommandTimeout = 10
            };
            await command.ExecuteNonQueryAsync();
        }

        if (createsOwnProductsTable)
        {
            return;
        }

        const string seedProducts = """
            INSERT INTO dbo.Products (Id, Name, CategoryId, Price, Stock, IsActive, DiscontinuedAt)
            VALUES
                (1, N'C# Basics', 1, 29.90, 15, 1, NULL),
                (2, N'SQL Server Guide', 1, 34.50, 8, 1, NULL),
                (3, N'RPG Dice Set', 2, 12.00, 30, 1, NULL),
                (4, N'Mechanical Keyboard', 3, 89.99, 5, 1, NULL),
                (5, N'Archived Mouse', 3, 19.90, 0, 0, N'2024-12-01');
            """;

        await using var productCommand = new SqlCommand(seedProducts, connection)
        {
            CommandTimeout = 10
        };
        await productCommand.ExecuteNonQueryAsync();
    }

    private static IReadOnlyList<string> SplitSqlStatements(string query)
    {
        if (Regex.IsMatch(query, @"^\s*DECLARE\b", RegexOptions.IgnoreCase))
        {
            return [query];
        }

        return query.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(statement => !string.IsNullOrWhiteSpace(statement))
            .ToList();
    }

    private static SqlTable? GetTable(string name)
    {
        var categories = new SqlTable(
            "Categories",
            ["Id", "Name"],
            [
                Row(("Id", 1), ("Name", "Books")),
                Row(("Id", 2), ("Name", "Games")),
                Row(("Id", 3), ("Name", "Hardware"))
            ]);

        var products = new SqlTable(
            "Products",
            ["Id", "Name", "CategoryId", "Price", "Stock", "IsActive", "DiscontinuedAt"],
            [
                Row(("Id", 1), ("Name", "C# Basics"), ("CategoryId", 1), ("Price", 29.90m), ("Stock", 15), ("IsActive", 1), ("DiscontinuedAt", "")),
                Row(("Id", 2), ("Name", "SQL Server Guide"), ("CategoryId", 1), ("Price", 34.50m), ("Stock", 8), ("IsActive", 1), ("DiscontinuedAt", "")),
                Row(("Id", 3), ("Name", "RPG Dice Set"), ("CategoryId", 2), ("Price", 12.00m), ("Stock", 30), ("IsActive", 1), ("DiscontinuedAt", "")),
                Row(("Id", 4), ("Name", "Mechanical Keyboard"), ("CategoryId", 3), ("Price", 89.99m), ("Stock", 5), ("IsActive", 1), ("DiscontinuedAt", "")),
                Row(("Id", 5), ("Name", "Archived Mouse"), ("CategoryId", 3), ("Price", 19.90m), ("Stock", 0), ("IsActive", 0), ("DiscontinuedAt", "2024-12-01"))
            ]);

        return string.Equals(name, "Categories", StringComparison.OrdinalIgnoreCase)
            ? categories
            : string.Equals(name, "Products", StringComparison.OrdinalIgnoreCase)
                ? products
                : null;
    }

    private static IReadOnlyDictionary<string, object> Row(params (string Key, object Value)[] values) =>
        values.ToDictionary(item => item.Key, item => item.Value, StringComparer.OrdinalIgnoreCase);

    private static List<string> ResolveColumns(string requested, IReadOnlyList<string> available)
    {
        if (requested.Trim() == "*")
        {
            return available.ToList();
        }

        var columns = requested
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(column => column.Trim('[', ']'))
            .ToList();

        return columns.All(column => available.Contains(column, StringComparer.OrdinalIgnoreCase))
            ? columns
            : [];
    }

    private static bool IsAggregateQuery(string selectedExpression, string groupBy) =>
        !string.IsNullOrWhiteSpace(groupBy) || Regex.IsMatch(selectedExpression, @"\b(COUNT|SUM|AVG|MIN|MAX)\s*\(", RegexOptions.IgnoreCase);

    private static SqlQueryResult ExecuteJoinQuery(Match match)
    {
        var productAlias = match.Groups["productAlias"].Value;
        var categoryAlias = match.Groups["categoryAlias"].Value;
        var joinType = Regex.Replace(match.Groups["join"].Value, @"\s+", " ").ToUpperInvariant();
        var on = Regex.Replace(match.Groups["on"].Value, @"\s+", " ").Trim();
        var expectedOn = $"{productAlias}.CategoryId = {categoryAlias}.Id";
        if (!string.Equals(on, expectedOn, StringComparison.OrdinalIgnoreCase))
        {
            return new SqlQueryResult(false, "", ["Condition ON non supportee pour ce scenario."], 0, [], []);
        }

        var products = GetTable("Products")!.Rows;
        var categories = GetTable("Categories")!.Rows;
        var joinedRows = new List<IReadOnlyDictionary<string, object>>();

        foreach (var product in products)
        {
            var category = categories.FirstOrDefault(item => Convert.ToInt32(item["Id"], CultureInfo.InvariantCulture) == Convert.ToInt32(product["CategoryId"], CultureInfo.InvariantCulture));
            if (category is not null || joinType is "LEFT" or "FULL OUTER")
            {
                joinedRows.Add(PrefixJoinRow(productAlias, product, categoryAlias, category));
            }
        }

        if (joinType is "RIGHT" or "FULL OUTER")
        {
            var matchedCategoryIds = products.Select(product => Convert.ToInt32(product["CategoryId"], CultureInfo.InvariantCulture)).ToHashSet();
            foreach (var category in categories.Where(category => !matchedCategoryIds.Contains(Convert.ToInt32(category["Id"], CultureInfo.InvariantCulture))))
            {
                joinedRows.Add(PrefixJoinRow(productAlias, null, categoryAlias, category));
            }
        }

        var filteredRows = joinedRows.Where(row => MatchesWhere(row, match.Groups["where"].Value)).ToList();
        var selected = ParseJoinColumns(match.Groups["columns"].Value, filteredRows.FirstOrDefault());
        if (selected.Count == 0)
        {
            return new SqlQueryResult(false, "", ["Colonnes de jointure non supportees."], 0, [], []);
        }

        var projectedRows = filteredRows.Select(row =>
            selected.ToDictionary(column => column.OutputName, column => row.TryGetValue(column.SourceName, out var value) ? value : "", StringComparer.OrdinalIgnoreCase) as IReadOnlyDictionary<string, object>)
            .ToList();
        var orderedRows = ApplyOrderBy(projectedRows, ResolveOrderColumn(match.Groups["order"].Value, selected), match.Groups["direction"].Value);
        var limitedRows = ApplyTop(orderedRows, match.Groups["top"].Value);
        var columns = selected.Select(column => column.OutputName).ToList();
        return new SqlQueryResult(true, FormatRows(columns, limitedRows), [], 0, columns, limitedRows);
    }

    private static IReadOnlyDictionary<string, object> PrefixJoinRow(string productAlias, IReadOnlyDictionary<string, object>? product, string categoryAlias, IReadOnlyDictionary<string, object>? category)
    {
        var row = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        foreach (var column in GetTable("Products")!.Columns)
        {
            row[$"{productAlias}.{column}"] = product is not null && product.TryGetValue(column, out var value) ? value : "";
        }

        foreach (var column in GetTable("Categories")!.Columns)
        {
            row[$"{categoryAlias}.{column}"] = category is not null && category.TryGetValue(column, out var value) ? value : "";
        }

        return row;
    }

    private static List<JoinColumn> ParseJoinColumns(string columns, IReadOnlyDictionary<string, object>? sampleRow) =>
        columns.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(column => ParseJoinColumn(column, sampleRow))
            .Where(column => column is not null)
            .Select(column => column!)
            .ToList();

    private static JoinColumn? ParseJoinColumn(string expression, IReadOnlyDictionary<string, object>? sampleRow)
    {
        var match = Regex.Match(expression, @"^(?<source>(?:\w+\.)?\w+)(?:\s+AS\s+(?<alias>\w+))?$", RegexOptions.IgnoreCase);
        if (!match.Success)
        {
            return null;
        }

        var source = match.Groups["source"].Value;
        if (sampleRow is not null && !sampleRow.ContainsKey(source))
        {
            var matchingKeys = sampleRow.Keys
                .Where(key => string.Equals(key.Split('.')[^1], source, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (matchingKeys.Count != 1)
            {
                return null;
            }

            source = matchingKeys[0];
        }

        var output = match.Groups["alias"].Success ? match.Groups["alias"].Value : source.Split('.')[^1];
        return new JoinColumn(source, output);
    }

    private static string ResolveOrderColumn(string orderBy, IReadOnlyList<JoinColumn> selected)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
        {
            return "";
        }

        var match = selected.FirstOrDefault(column =>
            string.Equals(column.SourceName, orderBy, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(column.OutputName, orderBy, StringComparison.OrdinalIgnoreCase));
        return match?.OutputName ?? orderBy.Split('.').Last();
    }

    private static SqlQueryResult ExecuteAggregateQuery(
        string selectedExpression,
        List<IReadOnlyDictionary<string, object>> sourceRows,
        string groupBy,
        string having,
        string orderBy,
        string direction,
        string top)
    {
        var expressions = selectedExpression
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(ParseSelectExpression)
            .ToList();

        if (expressions.Any(expression => expression is null))
        {
            return new SqlQueryResult(false, "", ["Expression SELECT agregee non supportee."], 0, [], []);
        }

        var parsedExpressions = expressions.Select(expression => expression!).ToList();
        if (!string.IsNullOrWhiteSpace(groupBy) && sourceRows.Any(row => !row.ContainsKey(groupBy)))
        {
            return new SqlQueryResult(false, "", [$"Colonne GROUP BY inconnue: {groupBy}"], 0, [], []);
        }

        var groups = string.IsNullOrWhiteSpace(groupBy)
            ? sourceRows.GroupBy(_ => "Total", StringComparer.OrdinalIgnoreCase)
            : sourceRows.GroupBy(row => Convert.ToString(row[groupBy], CultureInfo.InvariantCulture) ?? "", StringComparer.OrdinalIgnoreCase);

        var aggregateRows = new List<IReadOnlyDictionary<string, object>>();
        foreach (var group in groups)
        {
            var groupRows = group.ToList();
            if (!MatchesHaving(groupRows, having))
            {
                continue;
            }

            var row = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            foreach (var expression in parsedExpressions)
            {
                row[expression.OutputName] = expression.Kind == SelectExpressionKind.Column
                    ? GetColumnValue(groupRows.FirstOrDefault(), expression.ColumnName)
                    : EvaluateAggregate(expression.FunctionName!, expression.ColumnName, groupRows);
            }

            aggregateRows.Add(row);
        }

        var columns = parsedExpressions.Select(expression => expression.OutputName).ToList();
        var orderedRows = ApplyOrderBy(aggregateRows, orderBy, direction);
        var limitedRows = ApplyTop(orderedRows, top);
        return new SqlQueryResult(true, FormatRows(columns, limitedRows), [], 0, columns, limitedRows);
    }

    private static object GetColumnValue(IReadOnlyDictionary<string, object>? row, string? columnName) =>
        row is not null && !string.IsNullOrWhiteSpace(columnName) && row.TryGetValue(columnName, out var value) ? value : "";

    private static SelectExpression? ParseSelectExpression(string expression)
    {
        var aggregate = Regex.Match(expression, @"^(?<function>COUNT|SUM|AVG|MIN|MAX)\s*\(\s*(?<column>\*|\w+)\s*\)(?:\s+AS\s+(?<alias>\w+))?$", RegexOptions.IgnoreCase);
        if (aggregate.Success)
        {
            var functionName = aggregate.Groups["function"].Value.ToUpperInvariant();
            var columnName = aggregate.Groups["column"].Value == "*" ? null : aggregate.Groups["column"].Value;
            var alias = aggregate.Groups["alias"].Success ? aggregate.Groups["alias"].Value : $"{functionName}Value";
            return new SelectExpression(SelectExpressionKind.Aggregate, alias, columnName, functionName);
        }

        var column = Regex.Match(expression, @"^(?<column>\w+)(?:\s+AS\s+(?<alias>\w+))?$", RegexOptions.IgnoreCase);
        if (column.Success)
        {
            var columnName = column.Groups["column"].Value;
            var alias = column.Groups["alias"].Success ? column.Groups["alias"].Value : columnName;
            return new SelectExpression(SelectExpressionKind.Column, alias, columnName, null);
        }

        return null;
    }

    private static object EvaluateAggregate(string functionName, string? columnName, IReadOnlyList<IReadOnlyDictionary<string, object>> rows)
    {
        if (functionName == "COUNT")
        {
            return rows.Count;
        }

        if (string.IsNullOrWhiteSpace(columnName))
        {
            return 0;
        }

        var values = rows.Select(row => Convert.ToDecimal(row[columnName], CultureInfo.InvariantCulture)).ToList();
        return functionName switch
        {
            "SUM" => values.Sum(),
            "AVG" => Math.Round(values.Average(), 2),
            "MIN" => values.Min(),
            "MAX" => values.Max(),
            _ => 0
        };
    }

    private static bool MatchesHaving(IReadOnlyList<IReadOnlyDictionary<string, object>> rows, string having)
    {
        if (string.IsNullOrWhiteSpace(having))
        {
            return true;
        }

        var match = Regex.Match(having, @"^(?<function>COUNT|SUM|AVG|MIN|MAX)\s*\(\s*(?<column>\*|\w+)\s*\)\s*(?<op>=|>=|<=|>|<)\s*(?<value>\d+(?:\.\d+)?)$", RegexOptions.IgnoreCase);
        if (!match.Success)
        {
            return false;
        }

        var functionName = match.Groups["function"].Value.ToUpperInvariant();
        var columnName = match.Groups["column"].Value == "*" ? null : match.Groups["column"].Value;
        var actual = Convert.ToDecimal(EvaluateAggregate(functionName, columnName, rows), CultureInfo.InvariantCulture);
        var expected = Convert.ToDecimal(match.Groups["value"].Value, CultureInfo.InvariantCulture);
        return match.Groups["op"].Value switch
        {
            "=" => actual == expected,
            ">=" => actual >= expected,
            "<=" => actual <= expected,
            ">" => actual > expected,
            "<" => actual < expected,
            _ => false
        };
    }

    private static bool MatchesWhere(IReadOnlyDictionary<string, object> row, string where)
    {
        if (string.IsNullOrWhiteSpace(where))
        {
            return true;
        }

        var conditions = Regex.IsMatch(where, @"\s+BETWEEN\s+", RegexOptions.IgnoreCase)
            ? [where]
            : Regex.Split(where, @"\s+AND\s+", RegexOptions.IgnoreCase);
        return conditions.All(condition => MatchesCondition(row, condition.Trim()));
    }

    private static bool MatchesCondition(IReadOnlyDictionary<string, object> row, string condition)
    {
        var isNullMatch = Regex.Match(condition, @"^(?<column>(?:\w+\.)?\w+)\s+IS\s+(?<not>NOT\s+)?NULL$", RegexOptions.IgnoreCase);
        if (isNullMatch.Success)
        {
            var nullColumn = isNullMatch.Groups["column"].Value;
            var isNotNull = isNullMatch.Groups["not"].Success;
            var isNull = !row.TryGetValue(nullColumn, out var nullActual) || string.IsNullOrEmpty(Convert.ToString(nullActual, CultureInfo.InvariantCulture));
            return isNotNull ? !isNull : isNull;
        }

        var betweenMatch = Regex.Match(condition, @"^(?<column>(?:\w+\.)?\w+)\s+BETWEEN\s+(?<min>N?'[^']*'|\d+(?:\.\d+)?)\s+AND\s+(?<max>N?'[^']*'|\d+(?:\.\d+)?)$", RegexOptions.IgnoreCase);
        if (betweenMatch.Success)
        {
            var betweenColumn = betweenMatch.Groups["column"].Value;
            if (!row.TryGetValue(betweenColumn, out var betweenActual))
            {
                return false;
            }

            var betweenActualNumber = Convert.ToDecimal(betweenActual, CultureInfo.InvariantCulture);
            var min = Convert.ToDecimal(ParseValue(betweenMatch.Groups["min"].Value), CultureInfo.InvariantCulture);
            var max = Convert.ToDecimal(ParseValue(betweenMatch.Groups["max"].Value), CultureInfo.InvariantCulture);
            return betweenActualNumber >= min && betweenActualNumber <= max;
        }

        var inMatch = Regex.Match(condition, @"^(?<column>(?:\w+\.)?\w+)\s+IN\s*\((?<values>.+)\)$", RegexOptions.IgnoreCase);
        if (inMatch.Success)
        {
            var inColumn = inMatch.Groups["column"].Value;
            if (!row.TryGetValue(inColumn, out var inActual))
            {
                return false;
            }

            var values = inMatch.Groups["values"].Value
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(ParseValue);
            return values.Any(value => string.Equals(Convert.ToString(inActual, CultureInfo.InvariantCulture), Convert.ToString(value, CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase));
        }

        var likeMatch = Regex.Match(condition, @"^(?<column>(?:\w+\.)?\w+)\s+LIKE\s+(?<pattern>N?'[^']*')$", RegexOptions.IgnoreCase);
        if (likeMatch.Success)
        {
            var likeColumn = likeMatch.Groups["column"].Value;
            if (!row.TryGetValue(likeColumn, out var likeActual))
            {
                return false;
            }

            var pattern = Convert.ToString(ParseValue(likeMatch.Groups["pattern"].Value), CultureInfo.InvariantCulture) ?? "";
            var regex = "^" + Regex.Escape(pattern).Replace("%", ".*").Replace("_", ".") + "$";
            return Regex.IsMatch(Convert.ToString(likeActual, CultureInfo.InvariantCulture) ?? "", regex, RegexOptions.IgnoreCase);
        }

        var match = Regex.Match(condition, @"^(?<column>(?:\w+\.)?\w+)\s*(?<op>=|>=|<=|>|<)\s*(?<value>N?'[^']*'|\d+(?:\.\d+)?)$", RegexOptions.IgnoreCase);
        if (!match.Success)
        {
            return false;
        }

        var column = match.Groups["column"].Value;
        if (!row.TryGetValue(column, out var actual))
        {
            return false;
        }

        var expected = ParseValue(match.Groups["value"].Value);
        var op = match.Groups["op"].Value;

        if (actual is string actualText || expected is string)
        {
            var comparison = string.Compare(Convert.ToString(actual, CultureInfo.InvariantCulture), Convert.ToString(expected, CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase);
            return op == "=" && comparison == 0;
        }

        var actualNumber = Convert.ToDecimal(actual, CultureInfo.InvariantCulture);
        var expectedNumber = Convert.ToDecimal(expected, CultureInfo.InvariantCulture);
        return op switch
        {
            "=" => actualNumber == expectedNumber,
            ">=" => actualNumber >= expectedNumber,
            "<=" => actualNumber <= expectedNumber,
            ">" => actualNumber > expectedNumber,
            "<" => actualNumber < expectedNumber,
            _ => false
        };
    }

    private static object ParseValue(string value)
    {
        var normalized = value.StartsWith("N'", StringComparison.OrdinalIgnoreCase) ? value[1..] : value;
        if (normalized.StartsWith('\'') && normalized.EndsWith('\''))
        {
            return normalized.Trim('\'');
        }

        return decimal.Parse(normalized, CultureInfo.InvariantCulture);
    }

    private static List<IReadOnlyDictionary<string, object>> ApplyOrderBy(List<IReadOnlyDictionary<string, object>> rows, string column, string direction)
    {
        if (string.IsNullOrWhiteSpace(column) || rows.Count == 0 || !rows[0].ContainsKey(column))
        {
            return rows;
        }

        var ordered = rows.OrderBy(row => row[column] is string ? Convert.ToString(row[column], CultureInfo.InvariantCulture) : row[column]).ToList();
        return string.Equals(direction, "DESC", StringComparison.OrdinalIgnoreCase) ? ordered.AsEnumerable().Reverse().ToList() : ordered;
    }

    private static List<IReadOnlyDictionary<string, object>> ApplyDistinct(List<IReadOnlyDictionary<string, object>> rows, IReadOnlyList<string> columns) =>
        rows.GroupBy(row => string.Join("\u001f", columns.Select(column => Convert.ToString(row[column], CultureInfo.InvariantCulture))), StringComparer.OrdinalIgnoreCase)
            .Select(group => group.First())
            .ToList();

    private static List<IReadOnlyDictionary<string, object>> ApplyTop(List<IReadOnlyDictionary<string, object>> rows, string top) =>
        int.TryParse(top, out var count) ? rows.Take(count).ToList() : rows;

    private static string FormatRows(IReadOnlyList<string> columns, IReadOnlyList<IReadOnlyDictionary<string, object>> rows)
    {
        var lines = new List<string> { string.Join("\t", columns) };
        lines.AddRange(rows.Select(row => string.Join("\t", columns.Select(column => Convert.ToString(row[column], CultureInfo.InvariantCulture)))));
        return string.Join("\n", lines) + "\n";
    }

    private sealed record SqlTable(string Name, IReadOnlyList<string> Columns, IReadOnlyList<IReadOnlyDictionary<string, object>> Rows);

    private sealed record SelectExpression(SelectExpressionKind Kind, string OutputName, string? ColumnName, string? FunctionName);

    private sealed record JoinColumn(string SourceName, string OutputName);

    private enum SelectExpressionKind
    {
        Column,
        Aggregate
    }
}
