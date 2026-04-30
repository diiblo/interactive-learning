namespace CSharpInteractive.Api.Contracts;

public sealed record SqlColumnSchemaDto(string Name, string Type, bool IsNullable, string? Description);

public sealed record SqlTableSchemaDto(string Name, string Description, IReadOnlyList<SqlColumnSchemaDto> Columns);

public sealed record SqlSchemaDto(string Scenario, IReadOnlyList<SqlTableSchemaDto> Tables, IReadOnlyList<string> SafetyRules);

public sealed record SqlExecutionResultDto(
    bool Success,
    string Output,
    IReadOnlyList<string> Diagnostics,
    long DurationMs,
    IReadOnlyList<string> Columns,
    IReadOnlyList<IReadOnlyDictionary<string, object>> Rows);
