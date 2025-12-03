using Microsoft.AspNetCore.Identity;
using FormEODY.DataAccess.Entities;

namespace FormEODY.Web.Data;

public static class IdentitySeed
{
    public static async Task InitializeAsync(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        // Create roles
        string[] roles = new[] { "Editor", "Viewer" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Create Editor
        if (await userManager.FindByEmailAsync("editor@test.com") == null)
        {
            var user = new ApplicationUser
            {
                UserName = "editor@test.com",
                Email = "editor@test.com",
                EmailConfirmed = true,
                FirstName = "Editor",
                LastName = "User",
                UserType = 1
            };
            await userManager.CreateAsync(user, "Editor123!");
            await userManager.AddToRoleAsync(user, "Editor");
        }

        // Create Viewer
        if (await userManager.FindByEmailAsync("viewer@test.com") == null)
        {
            var user = new ApplicationUser
            {
                UserName = "viewer@test.com",
                Email = "viewer@test.com",
                EmailConfirmed = true,
                FirstName = "Viewer",
                LastName = "User",
                UserType = 2
            };
            await userManager.CreateAsync(user, "Viewer123!");
            await userManager.AddToRoleAsync(user, "Viewer");
        }
    }
}
