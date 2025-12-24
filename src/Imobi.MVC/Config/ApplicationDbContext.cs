using Imobi.Models.Empreendimento;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Imobi.Data.Identity;

namespace Imobi.Config;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // -----------------------------
    // Domínio RealEstate (schema realestate)
    // -----------------------------
    public DbSet<Empreendimento> Empreendimentos => Set<Empreendimento>();
    public DbSet<Condominio> Condominios => Set<Condominio>();
    public DbSet<InstalacaoCondominio> InstalacaoCondominios => Set<InstalacaoCondominio>();
    public DbSet<Arquivo> Arquivos => Set<Arquivo>();
    public DbSet<Unidade> Unidades => Set<Unidade>();
    public DbSet<Endereco> Enderecos => Set<Endereco>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // --------------------------------------
        // 🔷 1. Identity no schema "auth"
        // --------------------------------------
        builder.HasDefaultSchema("auth");

        builder.Entity<ApplicationUser>().ToTable("Usuarios");
        builder.Entity<IdentityRole>().ToTable("Perfis");
        builder.Entity<IdentityUserRole<string>>().ToTable("UsuariosPerfis");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UsuariosClaims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UsuariosLogins");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("PerfisClaims");
        builder.Entity<IdentityUserToken<string>>().ToTable("UsuariosTokens");

        // --------------------------------------
        // 🔶 2. Domínio organizado em schema "realestate"
        // --------------------------------------
        builder.Entity<Empreendimento>().ToTable("Empreendimentos", "realestate");
        builder.Entity<Condominio>().ToTable("Condominios", "realestate");
        builder.Entity<InstalacaoCondominio>().ToTable("InstalacaoCondominios", "realestate");
        builder.Entity<Arquivo>().ToTable("Arquivos", "realestate");
        builder.Entity<Unidade>().ToTable("Unidades", "realestate");
        builder.Entity<Endereco>().ToTable("Enderecos", "realestate");
    }

public DbSet<Imobi.Models.ViewModels.AdminUsuarios.UsuarioListItemViewModel> UsuarioListItemViewModel { get; set; } = default!;
}
