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
builder.Services.AddScoped<CourseCatalogService>();
builder.Services.AddScoped<SkillProgressService>();
builder.Services.AddScoped<LearningLanguageService>();
builder.Services.AddScoped<ILearningLanguageHandler, CSharpLearningLanguageHandler>();
builder.Services.AddScoped<ILearningLanguageHandler, SqlServerLearningLanguageHandler>();
builder.Services.AddScoped<ILearningLanguageHandler, PhpSymfonyLearningLanguageHandler>();

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

        CREATE TABLE IF NOT EXISTS "Skills" (
            "Id" INTEGER NOT NULL CONSTRAINT "PK_Skills" PRIMARY KEY AUTOINCREMENT,
            "CourseLanguage" TEXT NOT NULL,
            "Slug" TEXT NOT NULL,
            "Name" TEXT NOT NULL,
            "Description" TEXT NOT NULL
        );
        CREATE UNIQUE INDEX IF NOT EXISTS "IX_Skills_Slug" ON "Skills" ("Slug");

        CREATE TABLE IF NOT EXISTS "LessonSkills" (
            "LessonId" INTEGER NOT NULL,
            "SkillId" INTEGER NOT NULL,
            "Weight" INTEGER NOT NULL,
            CONSTRAINT "PK_LessonSkills" PRIMARY KEY ("LessonId", "SkillId"),
            CONSTRAINT "FK_LessonSkills_Lessons_LessonId" FOREIGN KEY ("LessonId") REFERENCES "Lessons" ("Id") ON DELETE CASCADE,
            CONSTRAINT "FK_LessonSkills_Skills_SkillId" FOREIGN KEY ("SkillId") REFERENCES "Skills" ("Id") ON DELETE CASCADE
        );
        CREATE INDEX IF NOT EXISTS "IX_LessonSkills_SkillId" ON "LessonSkills" ("SkillId");

        CREATE TABLE IF NOT EXISTS "LessonHints" (
            "Id" INTEGER NOT NULL CONSTRAINT "PK_LessonHints" PRIMARY KEY AUTOINCREMENT,
            "LessonId" INTEGER NOT NULL,
            "HintLevel" INTEGER NOT NULL,
            "Content" TEXT NOT NULL,
            CONSTRAINT "FK_LessonHints_Lessons_LessonId" FOREIGN KEY ("LessonId") REFERENCES "Lessons" ("Id") ON DELETE CASCADE
        );
        CREATE INDEX IF NOT EXISTS "IX_LessonHints_LessonId" ON "LessonHints" ("LessonId");


        CREATE TABLE IF NOT EXISTS "UserSkillProgress" (
            "Id" INTEGER NOT NULL CONSTRAINT "PK_UserSkillProgress" PRIMARY KEY AUTOINCREMENT,
            "UserProfileId" INTEGER NOT NULL,
            "SkillId" INTEGER NOT NULL,
            "MasteryPercent" INTEGER NOT NULL,
            "SuccessfulAttempts" INTEGER NOT NULL,
            "FailedAttempts" INTEGER NOT NULL,
            "NextReviewAt" TEXT NULL,
            "Status" INTEGER NOT NULL,
            CONSTRAINT "FK_UserSkillProgress_Skills_SkillId" FOREIGN KEY ("SkillId") REFERENCES "Skills" ("Id") ON DELETE CASCADE,
            CONSTRAINT "FK_UserSkillProgress_UserProfiles_UserProfileId" FOREIGN KEY ("UserProfileId") REFERENCES "UserProfiles" ("Id") ON DELETE CASCADE
        );
        CREATE INDEX IF NOT EXISTS "IX_UserSkillProgress_SkillId" ON "UserSkillProgress" ("SkillId");
        CREATE UNIQUE INDEX IF NOT EXISTS "IX_UserSkillProgress_UserProfileId_SkillId" ON "UserSkillProgress" ("UserProfileId", "SkillId");
        """);
    await db.Database.OpenConnectionAsync();
    using (var command = db.Database.GetDbConnection().CreateCommand())
    {
        command.CommandText = "SELECT COUNT(*) FROM pragma_table_info('Badges') WHERE name = 'RuleCourseLanguage';";
        var hasRuleCourseLanguageColumn = Convert.ToInt32(await command.ExecuteScalarAsync()) > 0;
        if (!hasRuleCourseLanguageColumn)
        {
            await db.Database.ExecuteSqlRawAsync("ALTER TABLE \"Badges\" ADD COLUMN \"RuleCourseLanguage\" TEXT NULL;");
        }
    }
    await SeedData.EnsureSeededAsync(db);
}

app.Run();
