using CSharpInteractive.Api.Models;
using CSharpInteractive.Api.Services;
using Xunit;

namespace CSharpInteractive.Api.Tests;

public sealed class LessonCorrectionServiceTests
{
    [Fact]
    public async Task SubmitAsync_PassesExpectedOutputAndSnippetTests()
    {
        var lesson = new Lesson
        {
            Tests =
            [
                new LessonTest { Name = "Output", TestType = LessonTestType.ExpectedOutput, ExpectedOutput = "Hello" },
                new LessonTest { Name = "Snippet", TestType = LessonTestType.RequiredSnippet, RequiredSnippet = "Console.WriteLine" }
            ]
        };
        var service = new LessonCorrectionService(new RoslynExecutionService());

        var result = await service.SubmitAsync(lesson, "using System; Console.WriteLine(\"Hello\");");

        Assert.True(result.Passed);
        Assert.All(result.Tests, test => Assert.True(test.Passed));
    }

    [Fact]
    public async Task SubmitAsync_ExecutesHiddenCodeTests()
    {
        var lesson = new Lesson
        {
            Tests =
            [
                new LessonTest
                {
                    Name = "Hidden",
                    TestType = LessonTestType.HiddenCode,
                    HiddenCode = "Console.WriteLine(Add(2, 3));",
                    ExpectedOutput = "5"
                }
            ]
        };
        var service = new LessonCorrectionService(new RoslynExecutionService());

        var result = await service.SubmitAsync(lesson, "using System; static int Add(int left, int right) => left + right;");

        Assert.True(result.Passed);
        Assert.All(result.Tests, test => Assert.True(test.Passed));
    }

    [Fact]
    public async Task SubmitAsync_FailsHiddenCodeTestsWhenExpectedOutputIsMissing()
    {
        var lesson = new Lesson
        {
            Tests =
            [
                new LessonTest
                {
                    Name = "Hidden",
                    TestType = LessonTestType.HiddenCode,
                    HiddenCode = "Console.WriteLine(Add(2, 3));",
                    ExpectedOutput = "5"
                }
            ]
        };
        var service = new LessonCorrectionService(new RoslynExecutionService());

        var result = await service.SubmitAsync(lesson, "using System; static int Add(int left, int right) => left - right;");

        Assert.False(result.Passed);
        Assert.Contains(result.Tests, test => test is { Name: "Hidden", Passed: false });
    }

    [Fact]
    public async Task SubmitAsync_ChecksMinimumSnippetCount()
    {
        var lesson = new Lesson
        {
            Tests =
            [
                new LessonTest
                {
                    Name = "Two writes",
                    TestType = LessonTestType.MinSnippetCount,
                    RequiredSnippet = "Console.WriteLine",
                    MinCount = 2
                }
            ]
        };
        var service = new LessonCorrectionService(new RoslynExecutionService());

        var result = await service.SubmitAsync(lesson, "using System; Console.WriteLine(\"A\"); Console.WriteLine(\"B\");");

        Assert.True(result.Passed);
        Assert.All(result.Tests, test => Assert.True(test.Passed));
    }
}
