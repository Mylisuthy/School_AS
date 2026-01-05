using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolAs.Application.DTOs;
using SchoolAs.Application.Interfaces;
using SchoolAs.Domain.Enums;

namespace SchoolAs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Secure by default
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> _userManager;

        public CoursesController(ICourseService courseService, Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager)
        {
            _courseService = courseService;
            _userManager = userManager;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> Search([FromQuery] string? q, [FromQuery] CourseStatus? status, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var courses = await _courseService.GetCoursesAsync(page, pageSize, status, q);
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetById(Guid id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null) return NotFound();

            // Enrich with Enrollment Status
            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            if (!string.IsNullOrEmpty(userEmail))
            {
                 var user = await _userManager.FindByEmailAsync(userEmail);
                 if (user != null)
                 {
                    course.IsEnrolled = await _courseService.IsEnrolledAsync(id, user.Id);
                 }
            }

            return Ok(course);
        }

        [HttpPost("{id}/enroll")]
        public async Task<IActionResult> Enroll(Guid id)
        {
            try
            {
                var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
                if (string.IsNullOrEmpty(userEmail)) return Unauthorized();

                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null) return Unauthorized();

                await _courseService.EnrollStudentAsync(id, user.Id); 
                return Ok(new { Message = "Enrolled successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> Create([FromBody] CreateCourseDto dto)
        {
            var course = await _courseService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourseDto dto)
        {
            try
            {
                await _courseService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _courseService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}/publish")]
        public async Task<IActionResult> Publish(Guid id)
        {
            try
            {
                await _courseService.PublishAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}/unpublish")]
        public async Task<IActionResult> Unpublish(Guid id)
        {
            try
            {
                await _courseService.UnpublishAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/summary")]
        public async Task<ActionResult<CourseSummaryDto>> GetSummary(Guid id)
        {
            var summary = await _courseService.GetSummaryAsync(id);
            if (summary == null) return NotFound();
            return Ok(summary);
        }
    }
}
