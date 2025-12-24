using Imobi.Data.Identity; // <--- AQUI ESTAVA O ERRO (Era Imobi.Models.Identity)
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration; // Adicionei para garantir o IConfiguration
using Microsoft.Extensions.DependencyInjection; // Adicionei para garantir o GetRequiredService

namespace Imobi.Config;

public static class IdentitySeed
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var configuration = services.GetRequiredService<IConfiguration>();

            var adminEmail = configuration["AdminUser:Email"] ?? "admin@imobi.com";
            var adminPassword = configuration["AdminUser:Password"] ?? "Admin@123";
            var nome = configuration["AdminUser:Nome"] ?? "Administrador";
            var sobrenome = configuration["AdminUser:SobreNome"] ?? "Sistema";

            const string adminRole = "Admin";

            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Nome = nome,
                    SobreNome = sobrenome,
                    Ativo = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (!result.Succeeded)
                {
                    throw new Exception("Erro ao criar admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            if (!await userManager.IsInRoleAsync(adminUser, adminRole))
            {
                await userManager.AddToRoleAsync(adminUser, adminRole);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro no Seed: {ex.Message}");
            throw;
        }
    }
}