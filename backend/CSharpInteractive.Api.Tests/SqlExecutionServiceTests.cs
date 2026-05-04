using CSharpInteractive.Api.Services;
using CSharpInteractive.Api.Options;
using Xunit;
using OptionsFactory = Microsoft.Extensions.Options.Options;

namespace CSharpInteractive.Api.Tests;

public sealed class SqlExecutionServiceTests
{
    [Fact]
    public async Task ExecuteQueryAsync_ReturnsSelectedColumnsAndRows()
    {
        var service = CreateService();

        var result = await service.ExecuteQueryAsync("SELECT Name, Price FROM Products;");

        Assert.True(result.Success);
        Assert.Equal(["Name", "Price"], result.Columns);
        Assert.Equal(5, result.Rows.Count);
        Assert.Contains("SQL Server Guide", result.Output);
    }

    [Fact]
    public async Task ExecuteQueryAsync_AppliesWhereConditions()
    {
        var service = CreateService();

        var result = await service.ExecuteQueryAsync("SELECT Name, Price, Stock FROM Products WHERE IsActive = 1 AND Stock >= 10;");

        Assert.True(result.Success);
        Assert.Equal(2, result.Rows.Count);
        Assert.Contains("C# Basics", result.Output);
        Assert.Contains("RPG Dice Set", result.Output);
    }

    [Fact]
    public async Task ExecuteQueryAsync_RejectsDangerousStatements()
    {
        var service = CreateService();

        var result = await service.ExecuteQueryAsync("DROP TABLE Products;");

        Assert.False(result.Success);
        Assert.NotEmpty(result.Diagnostics);
    }

    [Fact]
    public async Task ExecuteQueryAsync_AppliesOrderByAndTop()
    {
        var service = CreateService();

        var result = await service.ExecuteQueryAsync("SELECT TOP 2 Name, Price FROM Products WHERE IsActive = 1 ORDER BY Price DESC;");

        Assert.True(result.Success);
        Assert.Equal(2, result.Rows.Count);
        Assert.Contains("Mechanical Keyboard", result.Output);
        Assert.Contains("SQL Server Guide", result.Output);
    }

    [Fact]
    public async Task ExecuteQueryAsync_AppliesDistinct()
    {
        var service = CreateService();

        var result = await service.ExecuteQueryAsync("SELECT DISTINCT CategoryId FROM Products;");

        Assert.True(result.Success);
        Assert.Equal(3, result.Rows.Count);
    }

    [Theory]
    [InlineData("SELECT Name FROM Products WHERE Name LIKE '%SQL%';", 1)]
    [InlineData("SELECT Name, CategoryId FROM Products WHERE CategoryId IN (1, 2);", 3)]
    [InlineData("SELECT Name, Price FROM Products WHERE Price BETWEEN 20 AND 40;", 2)]
    [InlineData("SELECT Name, DiscontinuedAt FROM Products WHERE DiscontinuedAt IS NULL;", 4)]
    public async Task ExecuteQueryAsync_AppliesAdvancedFilters(string query, int expectedRows)
    {
        var service = CreateService();

        var result = await service.ExecuteQueryAsync(query);

        Assert.True(result.Success);
        Assert.Equal(expectedRows, result.Rows.Count);
    }

    [Theory]
    [InlineData("SELECT COUNT(*) AS ProductCount FROM Products;", "ProductCount", "5")]
    [InlineData("SELECT SUM(Stock) AS TotalStock FROM Products WHERE IsActive = 1;", "TotalStock", "58")]
    [InlineData("SELECT AVG(Price) AS AveragePrice FROM Products WHERE IsActive = 1;", "AveragePrice", "41.6")]
    [InlineData("SELECT MIN(Price) AS MinPrice, MAX(Price) AS MaxPrice FROM Products;", "MaxPrice", "89.99")]
    public async Task ExecuteQueryAsync_AppliesAggregateFunctions(string query, string expectedColumn, string expectedOutput)
    {
        var service = CreateService();

        var result = await service.ExecuteQueryAsync(query);

        Assert.True(result.Success);
        Assert.Contains(expectedColumn, result.Columns);
        Assert.Contains(expectedOutput, result.Output);
    }

    [Fact]
    public async Task ExecuteQueryAsync_AppliesGroupByAndHaving()
    {
        var service = CreateService();

        var result = await service.ExecuteQueryAsync("SELECT CategoryId, COUNT(*) AS ProductCount FROM Products GROUP BY CategoryId HAVING COUNT(*) >= 2 ORDER BY CategoryId;");

        Assert.True(result.Success);
        Assert.Equal(["CategoryId", "ProductCount"], result.Columns);
        Assert.Equal(2, result.Rows.Count);
        Assert.Contains("1\t2", result.Output);
        Assert.Contains("3\t2", result.Output);
    }

    [Theory]
    [InlineData("INNER JOIN")]
    [InlineData("LEFT JOIN")]
    [InlineData("RIGHT JOIN")]
    [InlineData("FULL OUTER JOIN")]
    public async Task ExecuteQueryAsync_AppliesJoins(string joinType)
    {
        var service = CreateService();

        var result = await service.ExecuteQueryAsync($"""
            SELECT p.Name AS ProductName, c.Name AS CategoryName
            FROM Products p
            {joinType} Categories c ON p.CategoryId = c.Id
            ORDER BY p.Name;
            """);

        Assert.True(result.Success);
        Assert.Equal(["ProductName", "CategoryName"], result.Columns);
        Assert.Equal(5, result.Rows.Count);
        Assert.Contains("C# Basics\tBooks", result.Output);
    }

    [Fact]
    public async Task ExecuteQueryAsync_AppliesTopWhereAndUnqualifiedColumnsInJoin()
    {
        var service = CreateService();

        var result = await service.ExecuteQueryAsync("""
            SELECT TOP 4 p.Name AS ProductName, c.Name AS CategoryName, Price
            FROM Products p
            INNER JOIN Categories c ON p.CategoryId = c.Id
            WHERE p.IsActive = 1
            ORDER BY p.Price DESC;
            """);

        Assert.True(result.Success);
        Assert.Equal(["ProductName", "CategoryName", "Price"], result.Columns);
        Assert.Equal(4, result.Rows.Count);
        Assert.Contains("Mechanical Keyboard\tHardware\t89.99", result.Output);
        Assert.Contains("SQL Server Guide\tBooks\t34.50", result.Output);
    }

    private static SqlExecutionService CreateService() =>
        new(new SqlSafetyService(), OptionsFactory.Create(new SqlLearningOptions()));
}
