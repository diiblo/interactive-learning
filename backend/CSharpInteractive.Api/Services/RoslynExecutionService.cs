using System.Diagnostics;
using System.Reflection;
using CSharpInteractive.Api.Contracts;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CSharpInteractive.Api.Services;

public sealed class RoslynExecutionService
{
    public async Task<ExecutionResultDto> ExecuteAsync(string code, int timeoutMs = 3000)
    {
        var stopwatch = Stopwatch.StartNew();
        var syntaxTree = CSharpSyntaxTree.ParseText($"using System;\n{code}");
        var references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => !assembly.IsDynamic && !string.IsNullOrWhiteSpace(assembly.Location))
            .Select(assembly => MetadataReference.CreateFromFile(assembly.Location))
            .Cast<MetadataReference>()
            .ToList();

        var compilation = CSharpCompilation.Create(
            $"Submission_{Guid.NewGuid():N}",
            [syntaxTree],
            references,
            new CSharpCompilationOptions(OutputKind.ConsoleApplication));

        using var assemblyStream = new MemoryStream();
        var emitResult = compilation.Emit(assemblyStream);

        if (!emitResult.Success)
        {
            stopwatch.Stop();
            var diagnostics = emitResult.Diagnostics
                .Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
                .Select(diagnostic => diagnostic.ToString())
                .ToList();

            return new ExecutionResultDto(false, "", diagnostics, stopwatch.ElapsedMilliseconds);
        }

        assemblyStream.Seek(0, SeekOrigin.Begin);
        var assembly = Assembly.Load(assemblyStream.ToArray());
        var entryPoint = assembly.EntryPoint;
        if (entryPoint is null)
        {
            return new ExecutionResultDto(false, "", ["Aucun point d'entree executable n'a ete trouve."], stopwatch.ElapsedMilliseconds);
        }

        var originalOut = Console.Out;
        using var writer = new StringWriter();
        Console.SetOut(writer);

        try
        {
            var task = Task.Run(async () =>
            {
                var parameters = entryPoint.GetParameters().Length == 0 ? null : new object[] { Array.Empty<string>() };
                var result = entryPoint.Invoke(null, parameters);
                if (result is Task asyncResult)
                {
                    await asyncResult;
                }
            });

            var completed = await Task.WhenAny(task, Task.Delay(timeoutMs));
            if (completed != task)
            {
                return new ExecutionResultDto(false, writer.ToString(), ["Temps d'execution depasse."], stopwatch.ElapsedMilliseconds);
            }

            await task;
            return new ExecutionResultDto(true, writer.ToString(), [], stopwatch.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return new ExecutionResultDto(false, writer.ToString(), [message], stopwatch.ElapsedMilliseconds);
        }
        finally
        {
            Console.SetOut(originalOut);
            stopwatch.Stop();
        }
    }
}
