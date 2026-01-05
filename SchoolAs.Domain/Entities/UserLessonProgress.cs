using Microsoft.AspNetCore.Identity;

namespace SchoolAs.Domain.Entities
{
    public class UserLessonProgress
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = string.Empty;
        public Guid LessonId { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime? CompletedAt { get; set; }

        // Navigation Properties
        public IdentityUser User { get; set; }
        public Lesson Lesson { get; set; }
    }
}
