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

        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetByCourseId(Guid courseId)
        {
            var lessons = await _lessonService.GetByCourseIdAsync(courseId);
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
            var lesson = await _lessonService.GetByIdAsync(id);
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
