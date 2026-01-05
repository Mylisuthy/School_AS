using System.Threading.Tasks;
using SchoolAs.Domain.Entities;
using SchoolAs.Domain.Enums;

namespace SchoolAs.Domain.Interfaces
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetCoursesAsync(int page, int pageSize, CourseStatus? status, string? searchTerm);
    }
}
