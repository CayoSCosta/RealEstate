using Imobi.Data.Identity; // Confirme se o namespace da pasta Identity é esse mesmo
using Imobi.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Imobi.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Empreendimento> Empreendimentos => Set<Empreendimento>();
        public DbSet<Arquivo> Arquivos => Set<Arquivo>();
        public DbSet<Unidade> Unidades => Set<Unidade>();
        public DbSet<Endereco> Enderecos => Set<Endereco>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("auth");
            builder.Entity<ApplicationUser>().ToTable("Usuarios");
            builder.Entity<IdentityRole>().ToTable("Perfis");
            builder.Entity<IdentityUserRole<string>>().ToTable("UsuariosPerfis");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UsuariosClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UsuariosLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("PerfisClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UsuariosTokens");

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}