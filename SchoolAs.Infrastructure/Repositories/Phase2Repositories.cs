using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolAs.Domain.Entities;
using SchoolAs.Domain.Interfaces;
using SchoolAs.Infrastructure.Persistence;

namespace SchoolAs.Infrastructure.Repositories
{
    public class UserCourseRepository : Repository<UserCourse>, IUserCourseRepository
    {
        public UserCourseRepository(ApplicationDbContext context) : base(context) { }

        public async Task<UserCourse?> GetByUserAndCourseAsync(string userId, Guid courseId)
        {
            return await _dbSet.FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == courseId);
        }

         public async Task<IEnumerable<UserCourse>> GetByCourseIdAsync(Guid courseId)
        {
             return await _dbSet.Where(uc => uc.CourseId == courseId).ToListAsync();
        }
    }

    public class UserLessonProgressRepository : Repository<UserLessonProgress>, IUserLessonProgressRepository
    {
        public UserLessonProgressRepository(ApplicationDbContext context) : base(context) { }

        public async Task<UserLessonProgress?> GetByUserAndLessonAsync(string userId, Guid lessonId)
        {
            return await _dbSet.FirstOrDefaultAsync(ulp => ulp.UserId == userId && ulp.LessonId == lessonId);
        }

         public async Task<IEnumerable<UserLessonProgress>> GetByUserIdAsync(string userId)
        {
             return await _dbSet.Where(ulp => ulp.UserId == userId).ToListAsync();
        }
    }
}
