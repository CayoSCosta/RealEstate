using Imobi.Models;
using Microsoft.EntityFrameworkCore;

namespace Imobi.Config;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Empreendimento> Empreendimentos => Set<Empreendimento>();
    public DbSet<Condominio> Condominios => Set<Condominio>();
    public DbSet<InstalacaoCondominio> InstalacaoCondominios => Set<InstalacaoCondominio>();
    public DbSet<Arquivo> Arquivos => Set<Arquivo>();
    public DbSet<Unidade> Unidades => Set<Unidade>();
    public DbSet<Endereco> Enderecos => Set<Endereco>();
}
