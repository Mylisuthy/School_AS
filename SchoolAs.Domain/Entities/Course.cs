using System;
using System.Collections.Generic;
using SchoolAs.Domain.Common;
using SchoolAs.Domain.Enums;

namespace SchoolAs.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; // New
        public string ImageUrl { get; set; } = string.Empty;    // New
        public CourseStatus Status { get; set; } = CourseStatus.Draft;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<UserCourse> Enrollments { get; set; } = new List<UserCourse>(); // New
    }
}
