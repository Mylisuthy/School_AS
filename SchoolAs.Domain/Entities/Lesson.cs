using System;
using SchoolAs.Domain.Common;

namespace SchoolAs.Domain.Entities
{
    public class Lesson : BaseEntity
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Order { get; set; }
        
        // Navigation Property
        public Course? Course { get; set; }
    }
}
