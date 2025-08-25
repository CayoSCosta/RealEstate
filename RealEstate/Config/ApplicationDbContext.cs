using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstate.Models.Entities.Empreendimento;
using RealEstate.Models.Entities.User;

namespace RealEstate.Config
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Arquivo> Arquivos => Set<Arquivo>();
        public DbSet<Condominio> Condominios => Set<Condominio>();
        public DbSet<Empreendimento> Empreendimentos => Set<Empreendimento>();
        public DbSet<Endereco> Enderecos => Set<Endereco>();
        public DbSet<Imagem> Imagens => Set<Imagem>();
        public DbSet<InstalacaoCondominio> InstalacoesCondominio => Set<InstalacaoCondominio>();
        public DbSet<Planta> Plantas => Set<Planta>();
        public DbSet<Unidade> Unidades => Set<Unidade>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Identity → schema "auth"
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers", "auth");
            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles", "auth");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles", "auth");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims", "auth");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins", "auth");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims", "auth");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens", "auth");

            // Empreendimentos → schema "imoveis"
            modelBuilder.Entity<Arquivo>().ToTable("Arquivos", "empreendimento");
            modelBuilder.Entity<Condominio>().ToTable("Condominios", "empreendimento");
            modelBuilder.Entity<Empreendimento>().ToTable("Empreendimentos", "empreendimento");
            modelBuilder.Entity<Endereco>().ToTable("Endereos", "empreendimento");
            modelBuilder.Entity<Imagem>().ToTable("Imagens", "empreendimento");
            modelBuilder.Entity<InstalacaoCondominio>().ToTable("InstalacaoCondominios", "empreendimento");
            modelBuilder.Entity<Planta>().ToTable("Plantas", "empreendimento");
            modelBuilder.Entity<Unidade>().ToTable("Unidades", "empreendimento");
        }

    }
}
