using Microsoft.AspNetCore.Identity;
using SchoolAs.Infrastructure.Persistence;

namespace SchoolAs.Api.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Ensure database is created
            context.Database.EnsureCreated();

            // Seed User
            var testUserEmail = "test@schoolas.com";
            var testUser = await userManager.FindByEmailAsync(testUserEmail);
            if (testUser == null)
            {
                testUser = new IdentityUser
                {
                    UserName = testUserEmail,
                    Email = testUserEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(testUser, "Password123!");
            }
        }
    }
}
