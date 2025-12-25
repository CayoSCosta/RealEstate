using Imobi.Application.Services;
using Imobi.Data.Context;
using Imobi.Data.Identity;
using Imobi.Data.Repositories;
using Imobi.Domain.Interfaces;
using Imobi.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Imobi.MVC.Config
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Conta/Login";
                options.AccessDeniedPath = "/Conta/AcessoNegado";
            });

            services.AddScoped<IEmpreendimentoRepository, EmpreendimentoRepository>();
            services.AddScoped<IUnidadeRepository, UnidadeRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IArquivoRepository, ArquivoRepository>();

            services.AddScoped<IEmpreendimentoService, EmpreendimentoService>();
            services.AddScoped<IUnidadeService, UnidadeService>();

            services.AddHttpClient<IEnderecoService, EnderecoService>();

            services.AddScoped<IImagemRepository, ImagemRepository>();

            services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);

            return services;
        }
    }
}