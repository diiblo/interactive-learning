using System.Net;
using System.Text;
using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Models;
using CSharpInteractive.Api.Options;
using CSharpInteractive.Api.Services;
using Xunit;
using OptionsFactory = Microsoft.Extensions.Options.Options;

namespace CSharpInteractive.Api.Tests;

public sealed class AiValidationServiceTests
{
    [Fact]
    public async Task ValidateLessonAsync_AcceptsOllamaJsonWithAlternatePropertyNames()
    {
        var lesson = new Lesson
        {
            Title = "Test",
            Objective = "Objective",
            ExercisePrompt = "Prompt",
            SuccessFeedback = "Success",
            Chapter = new Chapter
            {
                Course = new Course
                {
                    Title = "Cours",
                    Language = "csharp"
                }
            }
        };

        lesson.Tests.Add(new LessonTest { Name = "Output", SortOrder = 1 });

        var service = CreateService("""{"message":{"role":"assistant","content":"{\"correct\":true,\"message\":\"Bonne reponse\",\"tips\":[\"Verifie la sortie\"]}"}}""");
        var provider = new AiProviderConfigDto("1", "ollama-local", "Ollama local", "", "deepcoder:1.5b", "http://localhost:11434");

        var result = await service.ValidateLessonAsync(lesson, "using System;", [provider]);

        Assert.True(result.Passed);
        Assert.Equal("Bonne reponse", result.Feedback);
        Assert.Equal(["Verifie la sortie"], result.Hints);
        Assert.Equal("Ollama local", result.ProviderName);
    }

    [Fact]
    public async Task ValidateLessonAsync_RejectsLooseBooleanFallback()
    {
        var lesson = new Lesson
        {
            Title = "Test",
            Objective = "Objective",
            ExercisePrompt = "Prompt",
            SuccessFeedback = "Success",
            Chapter = new Chapter
            {
                Course = new Course
                {
                    Title = "Cours",
                    Language = "csharp"
                }
            }
        };

        var service = CreateService("""{"message":{"role":"assistant","content":"oui"}}""");
        var provider = new AiProviderConfigDto("1", "ollama-local", "Ollama local", "", "deepcoder:1.5b", "http://localhost:11434");

        var result = await service.ValidateLessonAsync(lesson, "using System;", [provider]);

        Assert.False(result.Passed);
        Assert.Contains("Aucun fournisseur IA", result.Feedback);
    }

    [Fact]
    public async Task ValidateLessonAsync_RecoversFromMalformedOllamaEnvelope()
    {
        var lesson = new Lesson
        {
            Title = "Test",
            Objective = "Objective",
            ExercisePrompt = "Prompt",
            SuccessFeedback = "Success",
            Chapter = new Chapter
            {
                Course = new Course
                {
                    Title = "Cours",
                    Language = "csharp"
                }
            }
        };

        var malformedBody = """{"message":{"content":"{\"correct\":true,\"message\":\"Bonne reponse\"}""";
        var service = CreateService(malformedBody);
        var provider = new AiProviderConfigDto("1", "ollama-local", "Ollama local", "", "deepcoder:1.5b", "http://localhost:11434");

        var result = await service.ValidateLessonAsync(lesson, "using System;", [provider]);

        Assert.True(result.Passed);
        Assert.Equal("Bonne reponse", result.Feedback);
    }

    private static AiValidationService CreateService(string responseContent)
    {
        var handler = new FixedResponseHandler(responseContent);
        var httpClient = new HttpClient(handler);
        var sqlExecutionService = new SqlExecutionService(new SqlSafetyService(), OptionsFactory.Create(new SqlLearningOptions()));
        return new AiValidationService(httpClient, sqlExecutionService);
    }

    private sealed class FixedResponseHandler(string content) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };

            return Task.FromResult(response);
        }
    }
}
