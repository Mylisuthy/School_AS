using System;
using System.Collections.Generic;
using SchoolAs.Domain.Common;
using SchoolAs.Domain.Enums;

namespace SchoolAs.Domain.Entities
{
    public class Course : BaseEntity
    {
        // Inherited: Id, IsDeleted, CreatedAt, UpdatedAt
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; 
        public string ImageUrl { get; set; } = string.Empty;    
        public CourseStatus Status { get; set; } = CourseStatus.Draft;

        // Navigation Properties
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<UserCourse> Enrollments { get; set; } = new List<UserCourse>();
    }
}
