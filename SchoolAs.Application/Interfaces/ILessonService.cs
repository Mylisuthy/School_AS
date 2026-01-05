using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolAs.Application.DTOs;

namespace SchoolAs.Application.Interfaces
{
    public interface ILessonService
    {
        Task<IEnumerable<LessonDto>> GetByCourseIdAsync(Guid courseId, string? userId = null);
        Task<LessonDto?> GetByIdAsync(Guid id, string? userId = null);
        Task<LessonDto> CreateAsync(CreateLessonDto dto);
        Task UpdateAsync(Guid id, UpdateLessonDto dto);
        Task DeleteAsync(Guid id);
        Task ReorderAsync(Guid courseId, List<Guid> lessonIdsInOrder); // Simplified reordering
        Task MarkLessonCompleteAsync(Guid lessonId, string userId); // New
        Task<bool> IsLessonCompletedAsync(Guid lessonId, string userId); // New
    }
}
