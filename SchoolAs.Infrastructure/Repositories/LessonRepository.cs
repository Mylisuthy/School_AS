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
    public class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        public LessonRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Lesson>> GetByCourseIdAsync(Guid courseId)
        {
            return await _dbSet
                .Where(l => l.CourseId == courseId)
                .OrderBy(l => l.Order)
                .ToListAsync();
        }
    }
}
