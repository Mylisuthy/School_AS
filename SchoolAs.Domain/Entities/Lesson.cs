using System;
using SchoolAs.Domain.Common;

namespace SchoolAs.Domain.Entities
{
    public class Lesson
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty; // New (Rich Text)
        public string VideoUrl { get; set; } = string.Empty; // New
        public int Order { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public Course? Course { get; set; }
        public ICollection<UserLessonProgress> UserProgress { get; set; } = new List<UserLessonProgress>(); // New
    }
}
