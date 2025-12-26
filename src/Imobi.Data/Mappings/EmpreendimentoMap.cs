using Imobi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imobi.Data.Mappings
{
    public class EmpreendimentoMap : IEntityTypeConfiguration<Empreendimento>
    {
        public void Configure(EntityTypeBuilder<Empreendimento> builder)
        {
            builder.ToTable("Empreendimentos", "realestate");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Sobre)
                .HasMaxLength(2000);

            builder.Property(e => e.AreaConstruida);


            builder.Property(e => e.Estagio)
                .HasMaxLength(100);

            // --- RELACIONAMENTOS ---

            builder.HasMany(e => e.Unidades)
                .WithOne(u => u.Empreendimento)
                .HasForeignKey(u => u.EmpreendimentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento 1:1 com Endereço
            builder.HasOne(e => e.Endereco)
                .WithOne()
                .HasForeignKey<Empreendimento>(e => e.EnderecoId);

            // Relacionamento com as novas Imagens (Galeria)
            builder.HasMany(e => e.Imagens)
                .WithOne(i => i.Empreendimento)
                .HasForeignKey(i => i.EmpreendimentoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}