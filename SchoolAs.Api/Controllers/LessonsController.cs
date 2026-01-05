using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolAs.Application.DTOs;
using SchoolAs.Application.Interfaces;

namespace SchoolAs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> _userManager;

        public LessonsController(ILessonService lessonService, Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager)
        {
            _lessonService = lessonService;
            _userManager = userManager;
        }

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetByCourseId(Guid courseId)
        {
            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            string? userId = null;
            
            if (!string.IsNullOrEmpty(userEmail))
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                userId = user?.Id;
            }

            var lessons = await _lessonService.GetByCourseIdAsync(courseId, userId);
            return Ok(lessons);
        }

        [HttpPost]
        public async Task<ActionResult<LessonDto>> Create([FromBody] CreateLessonDto dto)
        {
            try
            {
                var lesson = await _lessonService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = lesson.Id }, lesson);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDto>> GetById(Guid id)
        {
            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            string? userId = null;
            
            if (!string.IsNullOrEmpty(userEmail))
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                userId = user?.Id;
            }

            var lesson = await _lessonService.GetByIdAsync(id, userId);
            if (lesson == null) return NotFound();
            return Ok(lesson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLessonDto dto)
        {
            try
            {
                await _lessonService.UpdateAsync(id, dto);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _lessonService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> Complete(Guid id)
        {
            try
            {
                var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
                if (string.IsNullOrEmpty(userEmail)) return Unauthorized();

                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null) return Unauthorized();
                 
                await _lessonService.MarkLessonCompleteAsync(id, user.Id);
                return Ok(new { Message = "Lesson completed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("course/{courseId}/reorder")]
        public async Task<IActionResult> Reorder(Guid courseId, [FromBody] List<Guid> lessonIds)
        {
            try
            {
                await _lessonService.ReorderAsync(courseId, lessonIds);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
