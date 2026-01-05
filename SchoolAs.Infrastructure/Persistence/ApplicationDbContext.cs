using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolAs.Domain.Entities;

namespace SchoolAs.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; } // New
        public DbSet<UserLessonProgress> UserLessonProgresses { get; set; } // New

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Global Query Filters (Soft Delete)
            modelBuilder.Entity<Course>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Lesson>().HasQueryFilter(l => !l.IsDeleted);

            // Unique Order constraint per Course (for non-deleted lessons)
            modelBuilder.Entity<Lesson>()
                .HasIndex(l => new { l.CourseId, l.Order })
                .IsUnique()
                .HasFilter("\"IsDeleted\" = false");

            // UserCourse Configuration
            modelBuilder.Entity<UserCourse>()
                .HasOne(uc => uc.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(uc => uc.CourseId);

            // UserLessonProgress Configuration
            modelBuilder.Entity<UserLessonProgress>()
                .HasOne(ulp => ulp.Lesson)
                .WithMany(l => l.UserProgress)
                .HasForeignKey(ulp => ulp.LessonId);
        }
    }
}

