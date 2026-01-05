using Microsoft.AspNetCore.Identity;
using SchoolAs.Infrastructure.Persistence;

namespace SchoolAs.Api.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            int maxRetries = 10;
            int delaySeconds = 2;

            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    context.Database.EnsureCreated();

                    // 1. Seed Roles
                    string[] roleNames = { "Admin", "User" };
                    foreach (var roleName in roleNames)
                    {
                        var roleExist = await roleManager.RoleExistsAsync(roleName);
                        if (!roleExist)
                        {
                            await roleManager.CreateAsync(new IdentityRole(roleName));
                        }
                    }

                    // 2. Seed Admin User
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
                        var result = await userManager.CreateAsync(testUser, "Password123!");
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(testUser, "Admin");
                        }
                    }
                    else
                    {
                        // Ensure existing user has Admin role
                         if (!await userManager.IsInRoleAsync(testUser, "Admin"))
                        {
                            await userManager.AddToRoleAsync(testUser, "Admin");
                        }
                    }

                    // 3. Seed Regular User
                    var studentEmail = "student@schoolas.com";
                    var studentUser = await userManager.FindByEmailAsync(studentEmail);
                    if (studentUser == null)
                    {
                        studentUser = new IdentityUser
                        {
                            UserName = studentEmail,
                            Email = studentEmail,
                            EmailConfirmed = true
                        };
                        var result = await userManager.CreateAsync(studentUser, "Password123!");
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(studentUser, "User");
                        }
                    }

                    return; 
                }
                catch (Exception ex)
                {
                    if (i == maxRetries - 1) throw;
                    Console.WriteLine($"DB Connection failed (Attempt {i + 1}/{maxRetries}). Retrying in {delaySeconds}s... Error: {ex.Message}");
                    await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
                }
            }
        }
    }
}
