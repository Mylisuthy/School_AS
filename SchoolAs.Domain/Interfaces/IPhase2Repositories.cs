using SchoolAs.Domain.Entities;

namespace SchoolAs.Domain.Interfaces
{
    public interface IUserCourseRepository : IRepository<UserCourse>
    {
        Task<UserCourse?> GetByUserAndCourseAsync(string userId, Guid courseId);
         Task<IEnumerable<UserCourse>> GetByCourseIdAsync(Guid courseId);
    }

    public interface IUserLessonProgressRepository : IRepository<UserLessonProgress>
    {
        Task<UserLessonProgress?> GetByUserAndLessonAsync(string userId, Guid lessonId);
         Task<IEnumerable<UserLessonProgress>> GetByUserIdAsync(string userId);
    }
}
