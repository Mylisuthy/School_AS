using System;

namespace SchoolAs.Application.DTOs
{
    public class LessonDto
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty; // New
        public string VideoUrl { get; set; } = string.Empty; // New
        public int Order { get; set; }
        public bool IsCompleted { get; set; } // For UI
    }

    public class CreateLessonDto
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty; // New
        public string VideoUrl { get; set; } = string.Empty; // New
        public int Order { get; set; }
    }

    public class UpdateLessonDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty; // New
        public string VideoUrl { get; set; } = string.Empty; // New
        public int Order { get; set; }
    }
}
