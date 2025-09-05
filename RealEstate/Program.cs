using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using RealEstate.Config;
using RealEstate.Models.Entities.User;
using RealEstate.Services;
using Serilog;

namespace RealEstate;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Conexão PostgreSQL
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddControllersWithViews(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        });

        // MVC + filtro de autenticação global
        builder.Services.AddControllersWithViews(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        });


        builder.Services.AddHttpClient<ViaCepService>();

        // Razor Pages (necessário para login/register padrão)
        builder.Services.AddRazorPages();
        
        // Configura Serilog para gravar logs em arquivo
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information() // nível mínimo
            .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day) // novo arquivo a cada dia
            .CreateLogger();

        builder.Host.UseSerilog();

        var app = builder.Build();

        // Pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}",
            defaults: new { area = "Admin" });

        // Mapeia as páginas do Identity
        app.MapRazorPages();
        app.Run();
    }
}
