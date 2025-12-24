using Imobi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imobi.Data.Mappings
{
    public class ArquivoMap : IEntityTypeConfiguration<Arquivo>
    {
        public void Configure(EntityTypeBuilder<Arquivo> builder)
        {
            builder.ToTable("Arquivos", "realestate");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedNever();

            builder.Property(a => a.NomeArquivo).IsRequired().HasMaxLength(255);

            // Configura FKs opcionais
            builder.Property(a => a.EmpreendimentoId).IsRequired(false);
            builder.Property(a => a.UnidadeId).IsRequired(false);
        }
    }
}
