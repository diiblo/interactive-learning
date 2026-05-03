using CSharpInteractive.Api.Services;
using Xunit;

namespace CSharpInteractive.Api.Tests;

public sealed class RoslynExecutionServiceTests
{
    [Fact]
    public async Task ExecuteAsync_ReturnsConsoleOutput()
    {
        var service = new RoslynExecutionService();

        var result = await service.ExecuteAsync("using System; Console.WriteLine(\"Hello Tests\");");

        Assert.True(result.Success);
        Assert.Contains("Hello Tests", result.Output);
        Assert.Empty(result.Diagnostics);
    }

    [Fact]
    public async Task ExecuteAsync_AllowsConsoleWithoutExplicitUsing()
    {
        var service = new RoslynExecutionService();

        var result = await service.ExecuteAsync("Console.WriteLine(\"Bonjour C#\");");

        Assert.True(result.Success);
        Assert.Contains("Bonjour C#", result.Output);
        Assert.Empty(result.Diagnostics);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsDiagnosticsForInvalidCode()
    {
        var service = new RoslynExecutionService();

        var result = await service.ExecuteAsync("using System; Console.WriteLine(");

        Assert.False(result.Success);
        Assert.NotEmpty(result.Diagnostics);
    }
}
