using Imobi.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Imobi.Config;

public static class IdentitySeed
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        var adminEmail = configuration["AdminUser:Email"];
        var adminPassword = configuration["AdminUser:Password"];
        var nome = configuration["AdminUser:Nome"];
        var sobrenome = configuration["AdminUser:SobreNome"];

        const string adminRole = "Admin";

        // 1. Criar role Admin
        if (!await roleManager.RoleExistsAsync(adminRole))
        {
            await roleManager.CreateAsync(new IdentityRole(adminRole));
        }

        // 2. Criar usuário Admin
        var adminUser = await userManager.FindByEmailAsync(adminEmail!);

        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                Nome = nome!,
                SobreNome = sobrenome!
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword!);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        // 3. Associar à role Admin
        if (!await userManager.IsInRoleAsync(adminUser, adminRole))
        {
            await userManager.AddToRoleAsync(adminUser, adminRole);
        }
    }
}
