using SchoolAs.Domain.Common;

namespace SchoolAs.Domain.Entities
{
    public class UserCourse : BaseEntity
    {
        // Id, IsDeleted, CreatedAt, UpdatedAt inherited
        public string UserId { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Course Course { get; set; }
    }
}
