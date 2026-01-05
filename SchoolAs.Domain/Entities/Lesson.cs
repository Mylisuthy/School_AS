using System;
using SchoolAs.Domain.Common;

namespace SchoolAs.Domain.Entities
{
    public class Lesson : BaseEntity
    {
        // Inherited: Id, IsDeleted, CreatedAt, UpdatedAt
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty; 
        public string VideoUrl { get; set; } = string.Empty; 
        public int Order { get; set; }

        // Navigation Property
        public Course? Course { get; set; }
        public ICollection<UserLessonProgress> UserProgress { get; set; } = new List<UserLessonProgress>();
    }
}
