using Imobi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imobi.Data.Mappings
{
    public class ImagemMapping : IEntityTypeConfiguration<Imagem>
    {
        public void Configure(EntityTypeBuilder<Imagem> builder)
        {
            builder.ToTable("Imagens");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Base64)
                    .IsRequired()
                    .HasColumnType("text");

            builder.Property(c => c.Legenda)
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Tipo)
                .HasColumnType("varchar(50)");

            builder.HasOne(i => i.Empreendimento)
                .WithMany(e => e.Imagens)
                .HasForeignKey(i => i.EmpreendimentoId);

            builder.HasOne(i => i.Unidade)
                .WithMany(u => u.Imagens)
                .HasForeignKey(i => i.UnidadeId);
        }
    }
}
