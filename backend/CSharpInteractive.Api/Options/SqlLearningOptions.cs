namespace CSharpInteractive.Api.Options;

public sealed class SqlLearningOptions
{
    public bool UseSqlServer { get; set; }

    public string ConnectionString { get; set; } = "";
}
