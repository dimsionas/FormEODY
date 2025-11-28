using Microsoft.AspNetCore.Identity;

namespace FormEODY.Web.Data;

public static class IdentitySeed
{
    public static async Task InitializeAsync(
        UserManager<IdentityUser> userManager,
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
            var user = new IdentityUser
            {
                UserName = "editor@test.com",
                Email = "editor@test.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user, "Editor123!");
            await userManager.AddToRoleAsync(user, "Editor");
        }

        // Create Viewer
        if (await userManager.FindByEmailAsync("viewer@test.com") == null)
        {
            var user = new IdentityUser
            {
                UserName = "viewer@test.com",
                Email = "viewer@test.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user, "Viewer123!");
            await userManager.AddToRoleAsync(user, "Viewer");
        }
    }
}

