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

            int maxRetries = 10;
            int delaySeconds = 2;

            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
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
                    
                    // If successful, break the loop
                    return; 
                }
                catch (Exception ex)
                {
                    if (i == maxRetries - 1) throw; // Throw on last attempt
                    
                    Console.WriteLine($"DB Connection failed (Attempt {i + 1}/{maxRetries}). Retrying in {delaySeconds}s... Error: {ex.Message}");
                    await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
                }
            }
        }
    }
}
