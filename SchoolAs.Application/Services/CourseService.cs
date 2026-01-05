using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolAs.Application.DTOs;
using SchoolAs.Application.Interfaces;
using SchoolAs.Domain.Entities;
using SchoolAs.Domain.Enums;
using SchoolAs.Domain.Interfaces;

namespace SchoolAs.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILessonRepository _lessonRepository;

        public CourseService(ICourseRepository courseRepository, ILessonRepository lessonRepository)
        {
            _courseRepository = courseRepository;
            _lessonRepository = lessonRepository;
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesAsync(int page, int pageSize, CourseStatus? status, string? searchTerm)
        {
            var courses = await _courseRepository.GetCoursesAsync(page, pageSize, status, searchTerm);
            return courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description, // New
                ImageUrl = c.ImageUrl,       // New
                Status = c.Status,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            });
        }

        public async Task<CourseDto?> GetByIdAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return null;

            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description, // New
                ImageUrl = course.ImageUrl,       // New
                Status = course.Status,
                CreatedAt = course.CreatedAt,
                UpdatedAt = course.UpdatedAt
            };
        }

        public async Task<CourseDto> CreateAsync(CreateCourseDto dto)
        {
            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description, // New
                ImageUrl = dto.ImageUrl,       // New
                Status = CourseStatus.Draft
            };

            await _courseRepository.AddAsync(course);

            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description, // New
                ImageUrl = course.ImageUrl,       // New
                Status = course.Status,
                CreatedAt = course.CreatedAt,
                UpdatedAt = course.UpdatedAt
            };
        }

        public async Task UpdateAsync(Guid id, UpdateCourseDto dto)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) throw new Exception("Course not found");

            course.Title = dto.Title;
            course.Description = dto.Description; // New
            course.ImageUrl = dto.ImageUrl;       // New
            await _courseRepository.UpdateAsync(course);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _courseRepository.DeleteAsync(id);
        }

        public async Task PublishAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) throw new Exception("Course not found");

            var lessons = await _lessonRepository.GetByCourseIdAsync(id);
            if (!lessons.Any())
            {
                throw new InvalidOperationException("Cannot publish a course without lessons.");
            }

            course.Status = CourseStatus.Published;
            await _courseRepository.UpdateAsync(course);
        }

        public async Task UnpublishAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) throw new Exception("Course not found");

            course.Status = CourseStatus.Draft;
            await _courseRepository.UpdateAsync(course);
        }

        public async Task<CourseSummaryDto?> GetSummaryAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return null;

            var lessons = await _lessonRepository.GetByCourseIdAsync(id);

            return new CourseSummaryDto
            {
                Id = course.Id,
                Title = course.Title,
                TotalLessons = lessons.Count(),
                LastUpdatedAt = course.UpdatedAt
            };
        }
    }
}
