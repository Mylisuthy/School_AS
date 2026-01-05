using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAs.Application.DTOs;
using SchoolAs.Infrastructure.Persistence;

namespace SchoolAs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> _userManager;

        public DashboardController(ApplicationDbContext context, Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("stats")]
        public async Task<ActionResult<AdminStatsDto>> GetStats()
        {
            // 1. Total Users
            var totalUsers = await _userManager.Users.CountAsync();

            // 2. Total Enrollments
            var totalEnrollments = await _context.UserCourses.CountAsync();

            // 3. Top Courses logic
            // We need to calculate completions per course.
            // A completion is difficult to query directly because we don't have a 'CourseCompleted' flag in UserCourse.
            // Logic: A user completed a course if they have completed ALL lessons in that course.
            // OR simpler logic for "Successful Courses": Just rank by Enrollments for now if completion is too heavy, 
            // BUT the requirement asked for "Most successful courses" and "Number of users who completed".
            
            // Let's try to do it in memory for the top courses or use a smart query.
            // Fetching all courses with their lessons and user progress might be heavy, but let's try a projection.
            
            var coursesData = await _context.Courses
                .Select(c => new 
                {
                    c.Title,
                    EnrollmentCount = c.Enrollments.Count(),
                    LessonCount = c.Lessons.Count(),
                    // We can't easily count "Users who completed all lessons" in a single LINQ to SQL translation 
                    // without a defined relationship or detailed subquery that might fail translation.
                    // Let's grab the necessary IDs to calculate in memory for validity, 
                    // OR assume for now that "success" = enrollments, and we try to get global completions separately.
                })
                .OrderByDescending(x => x.EnrollmentCount)
                .Take(5)
                .ToListAsync();

            // To get accurate completion counts, we might need a raw SQL or a specific logic.
            // Given the constraints and likely low data volume for this school project, we can iterate.
            
            // Re-strategy: Get the top 5 courses by enrollment first.
            var topCourses = new List<CourseStatDto>();
            int globalCompletions = 0;

            // Let's get ALL enrollments and check completion for them? No, too big.
            // Let's try to get global completion count roughly: 
            // In a real app, we'd have a 'IsCompleted' on UserCourse. 
            // Since we don't, let's rely on the Top 5 for the detail and maybe just sum their completions for the "Total Completions" 
            // or leave Total Completions as 0 if it's too hard, essentially focusing on Enrollments.
            
            // WAIT! I wrote a query strategy in the plan using !Any(!IsCompleted). Let's try to implement that.
            // verify if EF Core 8 can translate it.
            
            var stats = await _context.Courses
                .Select(c => new CourseStatDto
                {
                    Title = c.Title,
                    Enrollments = c.Enrollments.Count(),
                    // Count users in Enrollments where...
                    Completions = c.Enrollments.Count(uc => 
                        // There are NO lessons in this course...
                        !c.Lessons.Any(l => 
                            // That do NOT have a straight completed record for this user
                            !l.UserProgress.Any(up => up.UserId == uc.UserId && up.IsCompleted)
                        )
                    )
                })
                .OrderByDescending(x => x.Completions) // Ranking by completions implies "success"? Or Enrollments? Let's sort by Enrollments then Completions.
                .ThenByDescending(x => x.Enrollments)
                .Take(5)
                .ToListAsync();

            // Global completions: sum of completions from all courses? 
            // We can't just sum the top 5. We need a global sum.
            // The query above gives us stats per course. If we want global, we'd need to run this over ALL courses.
            // Let's run it over all courses (assuming course count is low < 100)
            
            var allCourseStats = await _context.Courses
                 .Select(c => new 
                {
                    Completions = c.Enrollments.Count(uc => 
                        !c.Lessons.Any(l => 
                            !l.UserProgress.Any(up => up.UserId == uc.UserId && up.IsCompleted)
                        )
                    )
                })
                .ToListAsync();

            var totalCompletions = allCourseStats.Sum(x => x.Completions);

            return new AdminStatsDto
            {
                TotalUsers = totalUsers,
                TotalEnrollments = totalEnrollments,
                TotalCompletions = totalCompletions, // Sum of all calculated completions
                TopCourses = stats
            };
        }
    }
}
