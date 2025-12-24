using Imobi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imobi.Data.Mappings
{
    public class UnidadeMap : IEntityTypeConfiguration<Unidade>
    {
        public void Configure(EntityTypeBuilder<Unidade> builder)
        {
            builder.ToTable("Unidades", "realestate");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedNever();

            builder.Property(u => u.Valor).HasColumnType("decimal(18,2)");

            // FK explicita para garantir
            builder.HasOne(u => u.Empreendimento)
                .WithMany(e => e.Unidades)
                .HasForeignKey(u => u.EmpreendimentoId);
        }
    }
}
