using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolAs.Application.DTOs;
using SchoolAs.Application.Interfaces;
using SchoolAs.Domain.Entities;
using SchoolAs.Domain.Interfaces;

namespace SchoolAs.Application.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository _courseRepository;

        public LessonService(ILessonRepository lessonRepository, ICourseRepository courseRepository)
        {
            _lessonRepository = lessonRepository;
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<LessonDto>> GetByCourseIdAsync(Guid courseId)
        {
            var lessons = await _lessonRepository.GetByCourseIdAsync(courseId);
            return lessons.Select(l => new LessonDto
            {
                Id = l.Id,
                CourseId = l.CourseId,
                Title = l.Title,
                Content = l.Content,   // New
                VideoUrl = l.VideoUrl, // New
                Order = l.Order
            });
        }

        public async Task<LessonDto?> GetByIdAsync(Guid id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null) return null;

            return new LessonDto
            {
                Id = lesson.Id,
                CourseId = lesson.CourseId,
                Title = lesson.Title,
                Content = lesson.Content,   // New
                VideoUrl = lesson.VideoUrl, // New
                Order = lesson.Order
            };
        }

        public async Task<LessonDto> CreateAsync(CreateLessonDto dto)
        {
            var lessons = await _lessonRepository.GetByCourseIdAsync(dto.CourseId);
            if (lessons.Any(l => l.Order == dto.Order))
            {
                throw new InvalidOperationException("A lesson with this order already exists in the course.");
            }

            var lesson = new Lesson
            {
                CourseId = dto.CourseId,
                Title = dto.Title,
                Content = dto.Content,   // New
                VideoUrl = dto.VideoUrl, // New
                Order = dto.Order
            };

            await _lessonRepository.AddAsync(lesson);

            return new LessonDto
            {
                Id = lesson.Id,
                CourseId = lesson.CourseId,
                Title = lesson.Title,
                Content = lesson.Content,   // New
                VideoUrl = lesson.VideoUrl, // New
                Order = lesson.Order
            };
        }

        public async Task UpdateAsync(Guid id, UpdateLessonDto dto)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null) throw new Exception("Lesson not found");

            // Check order uniqueness only if order changed
            if (lesson.Order != dto.Order)
            {
                var existingLessons = await _lessonRepository.GetByCourseIdAsync(lesson.CourseId);
                if (existingLessons.Any(l => l.Order == dto.Order && l.Id != id))
                {
                   throw new InvalidOperationException("A lesson with this order already exists in the course.");
                }
            }

            lesson.Title = dto.Title;
            lesson.Content = dto.Content;   // New
            lesson.VideoUrl = dto.VideoUrl; // New
            lesson.Order = dto.Order;
            await _lessonRepository.UpdateAsync(lesson);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _lessonRepository.DeleteAsync(id);
        }

        public async Task ReorderAsync(Guid courseId, List<Guid> lessonIdsInOrder)
        {
            var lessons = (await _lessonRepository.GetByCourseIdAsync(courseId)).ToList();
            
            // Validate: check if all IDs belong to the course
            if (lessonIdsInOrder.Any(id => !lessons.Any(l => l.Id == id)))
            {
                throw new ArgumentException("One or more lesson IDs do not belong to the specified course.");
            }
            
            // Validate: check for duplicates in input
             if (lessonIdsInOrder.Count != lessonIdsInOrder.Distinct().Count())
            {
                throw new ArgumentException("Duplicate lesson IDs in order list.");
            }

            // Update orders
            for (int i = 0; i < lessonIdsInOrder.Count; i++)
            {
                var lesson = lessons.FirstOrDefault(l => l.Id == lessonIdsInOrder[i]);
                if (lesson != null)
                {
                    lesson.Order = i + 1; // 1-based order
                    await _lessonRepository.UpdateAsync(lesson);
                }
            }
        }
    }
}
