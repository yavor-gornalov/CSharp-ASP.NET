using Microsoft.AspNetCore.Identity;
using static HouseRentingSystem.Infrastructure.Common.AdministratorConstants;

namespace HouseRentingSystem.Extesions;

public static class ApplicationBuilderExtensions
{
    public static async Task CreateAdminRoleAsync(this IApplicationBuilder app)
    {
        using var scopedServices = app.ApplicationServices.CreateScope();

        var services = scopedServices.ServiceProvider;

        var userManager = services.GetRequiredService<UserManager<Infrastructure.Data.Models.ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        if (await roleManager.RoleExistsAsync(AdminRoleName))
        {
            return;
        }

        var role = new IdentityRole { Name = AdminRoleName };
        await roleManager.CreateAsync(role);

        var admin= await userManager.FindByEmailAsync(AdminEmail);
        if (admin != null)
        {
            await userManager.AddToRoleAsync(admin, AdminRoleName);
        }
    }
}
