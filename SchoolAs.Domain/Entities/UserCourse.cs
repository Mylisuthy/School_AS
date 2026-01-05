using Microsoft.AspNetCore.Identity;

namespace SchoolAs.Domain.Entities
{
    public class UserCourse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public IdentityUser User { get; set; }
        public Course Course { get; set; }
    }
}
