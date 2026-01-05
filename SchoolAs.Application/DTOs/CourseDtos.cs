using System;
using SchoolAs.Domain.Enums;

namespace SchoolAs.Application.DTOs
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; // New
        public string ImageUrl { get; set; } = string.Empty; // New
        public CourseStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsEnrolled { get; set; } // For UI
        public int EnrollmentCount { get; set; } // For Admin
    }

    public class CreateCourseDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; // New
        public string ImageUrl { get; set; } = string.Empty; // New
    }

    public class UpdateCourseDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; // New
        public string ImageUrl { get; set; } = string.Empty; // New
    }

    public class CourseSummaryDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int TotalLessons { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }
}
