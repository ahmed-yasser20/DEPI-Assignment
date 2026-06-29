using BookStoreAPI.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookStoreAPI.Data;

public static class DbSeeder
{
    public static async Task SeedRolesAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = ["Admin", "Customer"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    public static async Task SeedAdminAsync(IServiceProvider services, IConfiguration config)
    {
        var userManager = services.GetRequiredService<UserManager<AppUser>>();

        var adminEmail = config["AdminSeed:Email"];
        var adminPassword = config["AdminSeed:Password"];

        if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPassword))
            return;

        var existing = await userManager.FindByEmailAsync(adminEmail);
        if (existing != null) return;

        var admin = new AppUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "Super",
            LastName = "Admin",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(admin, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Admin account seeded: {Email}", adminEmail);
        }
    }
}
