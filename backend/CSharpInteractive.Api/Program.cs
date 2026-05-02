using CSharpInteractive.Api.Data;
using CSharpInteractive.Api.Options;
using CSharpInteractive.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SqlLearningOptions>(builder.Configuration.GetSection("SqlLearning"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=interactive-learning.db"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddScoped<RoslynExecutionService>();
builder.Services.AddScoped<LessonCorrectionService>();
builder.Services.AddScoped<SqlSafetyService>();
builder.Services.AddScoped<SqlExecutionService>();
builder.Services.AddScoped<SqlCorrectionService>();
builder.Services.AddScoped<IntermediateBossService>();
builder.Services.AddScoped<PhpSymfonyValidationService>();
builder.Services.AddScoped<ProgressService>();
builder.Services.AddScoped<UnlockService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Frontend");
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
    await db.Database.ExecuteSqlRawAsync(
        """
        CREATE TABLE IF NOT EXISTS "IntermediateBosses" (
            "Id" INTEGER NOT NULL CONSTRAINT "PK_IntermediateBosses" PRIMARY KEY AUTOINCREMENT,
            "ModuleId" INTEGER NOT NULL,
            "Slug" TEXT NOT NULL,
            "Title" TEXT NOT NULL,
            "Objective" TEXT NOT NULL,
            "Instructions" TEXT NOT NULL,
            "StarterCode" TEXT NOT NULL,
            "ExpectedResult" TEXT NOT NULL,
            "Solution" TEXT NOT NULL,
            "XpReward" INTEGER NOT NULL,
            "IsRequiredToUnlockNextModule" INTEGER NOT NULL,
            CONSTRAINT "FK_IntermediateBosses_Chapters_ModuleId" FOREIGN KEY ("ModuleId") REFERENCES "Chapters" ("Id") ON DELETE CASCADE
        );
        CREATE UNIQUE INDEX IF NOT EXISTS "IX_IntermediateBosses_Slug" ON "IntermediateBosses" ("Slug");
        CREATE UNIQUE INDEX IF NOT EXISTS "IX_IntermediateBosses_ModuleId" ON "IntermediateBosses" ("ModuleId");

        CREATE TABLE IF NOT EXISTS "IntermediateBossHints" (
            "Id" INTEGER NOT NULL CONSTRAINT "PK_IntermediateBossHints" PRIMARY KEY AUTOINCREMENT,
            "IntermediateBossId" INTEGER NOT NULL,
            "Content" TEXT NOT NULL,
            "SortOrder" INTEGER NOT NULL,
            CONSTRAINT "FK_IntermediateBossHints_IntermediateBosses_IntermediateBossId" FOREIGN KEY ("IntermediateBossId") REFERENCES "IntermediateBosses" ("Id") ON DELETE CASCADE
        );
        CREATE INDEX IF NOT EXISTS "IX_IntermediateBossHints_IntermediateBossId" ON "IntermediateBossHints" ("IntermediateBossId");

        CREATE TABLE IF NOT EXISTS "IntermediateBossValidationRules" (
            "Id" INTEGER NOT NULL CONSTRAINT "PK_IntermediateBossValidationRules" PRIMARY KEY AUTOINCREMENT,
            "IntermediateBossId" INTEGER NOT NULL,
            "Name" TEXT NOT NULL,
            "TestType" INTEGER NOT NULL,
            "ExpectedOutput" TEXT NULL,
            "RequiredSnippet" TEXT NULL,
            "HiddenCode" TEXT NULL,
            "MinCount" INTEGER NULL,
            "ExpectedColumns" TEXT NULL,
            "ExpectedRowCount" INTEGER NULL,
            "SortOrder" INTEGER NOT NULL,
            CONSTRAINT "FK_IntermediateBossValidationRules_IntermediateBosses_IntermediateBossId" FOREIGN KEY ("IntermediateBossId") REFERENCES "IntermediateBosses" ("Id") ON DELETE CASCADE
        );
        CREATE INDEX IF NOT EXISTS "IX_IntermediateBossValidationRules_IntermediateBossId" ON "IntermediateBossValidationRules" ("IntermediateBossId");

        CREATE TABLE IF NOT EXISTS "IntermediateBossProgress" (
            "Id" INTEGER NOT NULL CONSTRAINT "PK_IntermediateBossProgress" PRIMARY KEY AUTOINCREMENT,
            "UserProfileId" INTEGER NOT NULL,
            "IntermediateBossId" INTEGER NOT NULL,
            "Status" INTEGER NOT NULL,
            "BestCode" TEXT NULL,
            "LastOutput" TEXT NULL,
            "Attempts" INTEGER NOT NULL,
            "FailedAttempts" INTEGER NOT NULL,
            "HintsRevealed" INTEGER NOT NULL,
            "CompletedAt" TEXT NULL,
            "EarnedXp" INTEGER NOT NULL,
            CONSTRAINT "FK_IntermediateBossProgress_IntermediateBosses_IntermediateBossId" FOREIGN KEY ("IntermediateBossId") REFERENCES "IntermediateBosses" ("Id") ON DELETE CASCADE,
            CONSTRAINT "FK_IntermediateBossProgress_UserProfiles_UserProfileId" FOREIGN KEY ("UserProfileId") REFERENCES "UserProfiles" ("Id") ON DELETE CASCADE
        );
        CREATE INDEX IF NOT EXISTS "IX_IntermediateBossProgress_IntermediateBossId" ON "IntermediateBossProgress" ("IntermediateBossId");
        CREATE UNIQUE INDEX IF NOT EXISTS "IX_IntermediateBossProgress_UserProfileId_IntermediateBossId" ON "IntermediateBossProgress" ("UserProfileId", "IntermediateBossId");
        """);
    await SeedData.EnsureSeededAsync(db);
}

app.Run();
