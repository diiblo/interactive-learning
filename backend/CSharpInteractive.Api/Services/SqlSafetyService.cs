using System.Text.RegularExpressions;

namespace CSharpInteractive.Api.Services;

public sealed class SqlSafetyService
{
    private static readonly string[] ForbiddenTokens =
    [
        "DROP DATABASE",
        "DROP TABLE",
        "TRUNCATE",
        "ALTER SERVER",
        "SYS.",
        "INFORMATION_SCHEMA"
    ];

    public IReadOnlyList<string> ValidateReadOnlyModuleQuery(string query)
    {
        var diagnostics = new List<string>();
        var normalized = Regex.Replace(query, @"\s+", " ").Trim().ToUpperInvariant();
        var statements = normalized.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var hasControlledWrite = Regex.IsMatch(normalized, @"\b(CREATE\s+(TABLE|INDEX|VIEW|PROCEDURE|FUNCTION)|DECLARE|INSERT|UPDATE|DELETE|BEGIN\s+TRAN|COMMIT|ROLLBACK)\b", RegexOptions.IgnoreCase);
        if (statements.Length > 1 && !hasControlledWrite)
        {
            diagnostics.Add("Les requetes multi-statements sont reservees aux exercices de modification ou de transaction.");
        }

        foreach (var token in ForbiddenTokens)
        {
            if (normalized.Contains(token, StringComparison.OrdinalIgnoreCase))
            {
                diagnostics.Add($"Operation interdite detectee: {token}");
            }
        }

        foreach (var statement in statements)
        {
            if (Regex.IsMatch(statement, @"^UPDATE\s+\w+\s+SET\s+", RegexOptions.IgnoreCase) &&
                !Regex.IsMatch(statement, @"\bWHERE\b", RegexOptions.IgnoreCase))
            {
                diagnostics.Add("UPDATE doit contenir une clause WHERE dans les exercices.");
            }

            if (Regex.IsMatch(statement, @"^DELETE\s+FROM\s+\w+", RegexOptions.IgnoreCase) &&
                !Regex.IsMatch(statement, @"\bWHERE\b", RegexOptions.IgnoreCase))
            {
                diagnostics.Add("DELETE doit contenir une clause WHERE dans les exercices.");
            }

            if (Regex.IsMatch(statement, @"^EXEC\b", RegexOptions.IgnoreCase) &&
                !Regex.IsMatch(statement, @"^EXEC\s+(dbo\.)?GetActiveProducts\b", RegexOptions.IgnoreCase))
            {
                diagnostics.Add("EXEC est autorise uniquement pour la procedure pedagogique GetActiveProducts.");
            }
        }

        return diagnostics;
    }
}
