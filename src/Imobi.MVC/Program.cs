using Imobi.Config; // Se tiver o IdentitySeed aqui
using Imobi.Middleware;
using Imobi.MVC.Config;
using Serilog;

SerilogConfig.Configure();
Log.Information("Serilog inicializado com sucesso!");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.ResolveDependencies(builder.Configuration);

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseMiddleware<ClaimAuthorizationMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Conta}/{action=Login}");

await IdentitySeed.SeedAsync(app.Services);

app.Run();