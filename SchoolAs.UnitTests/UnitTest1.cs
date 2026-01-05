using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using SchoolAs.Application.DTOs;
using SchoolAs.Application.Services;
using SchoolAs.Domain.Entities;
using SchoolAs.Domain.Enums;
using SchoolAs.Domain.Interfaces;
using Xunit;

namespace SchoolAs.UnitTests
{
    public class BusinessLogicTests
    {
        private readonly Mock<ICourseRepository> _courseRepoMock;
        private readonly Mock<ILessonRepository> _lessonRepoMock;
        private readonly CourseService _courseService;
        private readonly Mock<ICourseRepository> _courseRepoMockForLesson;
        private readonly LessonService _lessonService;

        public BusinessLogicTests()
        {
            _courseRepoMock = new Mock<ICourseRepository>();
            _lessonRepoMock = new Mock<ILessonRepository>();
            _courseService = new CourseService(_courseRepoMock.Object, _lessonRepoMock.Object);
            
            _courseRepoMockForLesson = new Mock<ICourseRepository>();
            _lessonService = new LessonService(_lessonRepoMock.Object, _courseRepoMockForLesson.Object);
        }

        [Fact]
        public async Task PublishCourse_WithLessons_ShouldSucceed()
        {
            // Arrange
            var courseId = Guid.NewGuid();
            var course = new Course { Id = courseId, Status = CourseStatus.Draft };
            var lessons = new List<Lesson> { new Lesson { Id = Guid.NewGuid(), CourseId = courseId } };

            _courseRepoMock.Setup(r => r.GetByIdAsync(courseId)).ReturnsAsync(course);
            _lessonRepoMock.Setup(r => r.GetByCourseIdAsync(courseId)).ReturnsAsync(lessons);

            // Act
            await _courseService.PublishAsync(courseId);

            // Assert
            Assert.Equal(CourseStatus.Published, course.Status);
            _courseRepoMock.Verify(r => r.UpdateAsync(course), Times.Once);
        }

        [Fact]
        public async Task PublishCourse_WithoutLessons_ShouldFail()
        {
            // Arrange
            var courseId = Guid.NewGuid();
            var course = new Course { Id = courseId, Status = CourseStatus.Draft };
            var lessons = new List<Lesson>(); // Empty

            _courseRepoMock.Setup(r => r.GetByIdAsync(courseId)).ReturnsAsync(course);
            _lessonRepoMock.Setup(r => r.GetByCourseIdAsync(courseId)).ReturnsAsync(lessons);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _courseService.PublishAsync(courseId));
        }

        [Fact]
        public async Task CreateLesson_WithUniqueOrder_ShouldSucceed()
        {
            // Arrange
            var courseId = Guid.NewGuid();
            var existingLessons = new List<Lesson> 
            { 
                new Lesson { Order = 1, CourseId = courseId } 
            };
            var newLessonDto = new CreateLessonDto { CourseId = courseId, Order = 2, Title = "New Lesson" };

            _lessonRepoMock.Setup(r => r.GetByCourseIdAsync(courseId)).ReturnsAsync(existingLessons);

            // Act
            await _lessonService.CreateAsync(newLessonDto);

            // Assert
            _lessonRepoMock.Verify(r => r.AddAsync(It.Is<Lesson>(l => l.Order == 2)), Times.Once);
        }

        [Fact]
        public async Task CreateLesson_WithDuplicateOrder_ShouldFail()
        {
            // Arrange
            var courseId = Guid.NewGuid();
            var existingLessons = new List<Lesson> 
            { 
                new Lesson { Order = 1, CourseId = courseId } 
            };
            var newLessonDto = new CreateLessonDto { CourseId = courseId, Order = 1, Title = "Duplicate Order Lesson" };

            _lessonRepoMock.Setup(r => r.GetByCourseIdAsync(courseId)).ReturnsAsync(existingLessons);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _lessonService.CreateAsync(newLessonDto));
        }

        [Fact]
        public async Task DeleteCourse_ShouldBeSoftDelete()
        {
            // Arrange
            var courseId = Guid.NewGuid();
            
            // Act
            await _courseService.DeleteAsync(courseId);

            // Assert
            // The service calls repo.DeleteAsync(id). The repo implementation handles soft delete.
            // Since we mocked the interface, we verify the service calls the repo method.
            // In a real integration test we would check the IsDeleted flag.
            _courseRepoMock.Verify(r => r.DeleteAsync(courseId), Times.Once);
        }
    }
}