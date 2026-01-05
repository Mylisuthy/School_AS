using System;
using SchoolAs.Domain.Enums;

namespace SchoolAs.Application.DTOs
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public CourseStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateCourseDto
    {
        public string Title { get; set; } = string.Empty;
    }

    public class UpdateCourseDto
    {
        public string Title { get; set; } = string.Empty;
    }

    public class CourseSummaryDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int TotalLessons { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }
}
