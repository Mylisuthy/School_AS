using SchoolAs.Domain.Common;

namespace SchoolAs.Domain.Entities
{
    public class UserLessonProgress : BaseEntity
    {
        // Id, IsDeleted, CreatedAt, UpdatedAt inherited
        public string UserId { get; set; } = string.Empty;
        public Guid LessonId { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime? CompletedAt { get; set; }

        // Navigation Properties
        public Lesson Lesson { get; set; }
    }
}
