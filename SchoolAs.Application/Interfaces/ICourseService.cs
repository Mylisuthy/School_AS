using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolAs.Application.DTOs;
using SchoolAs.Domain.Enums;

namespace SchoolAs.Application.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetCoursesAsync(int page, int pageSize, CourseStatus? status, string? searchTerm);
        Task<CourseDto?> GetByIdAsync(Guid id);
        Task<CourseDto> CreateAsync(CreateCourseDto dto);
        Task UpdateAsync(Guid id, UpdateCourseDto dto);
        Task DeleteAsync(Guid id);
        Task PublishAsync(Guid id);
        Task UnpublishAsync(Guid id);
        Task<CourseSummaryDto?> GetSummaryAsync(Guid id);
    }
}
