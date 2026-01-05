namespace SchoolAs.Application.DTOs
{
    public class AdminStatsDto
    {
        public int TotalUsers { get; set; }
        public int TotalEnrollments { get; set; }
        public int TotalCompletions { get; set; }
        public List<CourseStatDto> TopCourses { get; set; } = new();
    }

    public class CourseStatDto
    {
        public string Title { get; set; } = string.Empty;
        public int Enrollments { get; set; }
        public int Completions { get; set; }
    }
}
