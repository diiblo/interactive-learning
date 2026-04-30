using CSharpInteractive.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpInteractive.Api.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Chapter> Chapters => Set<Chapter>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<LessonTest> LessonTests => Set<LessonTest>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<LessonProgress> LessonProgress => Set<LessonProgress>();
    public DbSet<Badge> Badges => Set<Badge>();
    public DbSet<UserBadge> UserBadges => Set<UserBadge>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasIndex(course => course.Slug).IsUnique();
        modelBuilder.Entity<Lesson>().HasIndex(lesson => lesson.Slug).IsUnique();
        modelBuilder.Entity<Badge>().HasIndex(badge => badge.Slug).IsUnique();

        modelBuilder.Entity<LessonProgress>()
            .HasIndex(progress => new { progress.UserProfileId, progress.LessonId })
            .IsUnique();

        modelBuilder.Entity<UserBadge>()
            .HasIndex(userBadge => new { userBadge.UserProfileId, userBadge.BadgeId })
            .IsUnique();

        modelBuilder.Entity<Course>()
            .HasMany(course => course.Chapters)
            .WithOne(chapter => chapter.Course)
            .HasForeignKey(chapter => chapter.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Chapter>()
            .HasMany(chapter => chapter.Lessons)
            .WithOne(lesson => lesson.Chapter)
            .HasForeignKey(lesson => lesson.ChapterId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Lesson>()
            .HasMany(lesson => lesson.Tests)
            .WithOne(test => test.Lesson)
            .HasForeignKey(test => test.LessonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
