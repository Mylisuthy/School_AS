using System;
using System.Threading.Tasks;
using SchoolAs.Domain.Entities;

namespace SchoolAs.Domain.Interfaces
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Task<IEnumerable<Lesson>> GetByCourseIdAsync(Guid courseId);
    }
}
