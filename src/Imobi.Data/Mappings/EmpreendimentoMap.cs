using Imobi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imobi.Data.Mappings
{
    public class EmpreendimentoMap : IEntityTypeConfiguration<Empreendimento>
    {
        public void Configure(EntityTypeBuilder<Empreendimento> builder)
        {
            builder.ToTable("Empreendimentos", "realestate");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Nome).IsRequired().HasMaxLength(200);

            builder.Property(e => e.Sobre).HasMaxLength(2000);

            builder.HasMany(e => e.Unidades)
                .WithOne(u => u.Empreendimento)
                .HasForeignKey(u => u.EmpreendimentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento 1:1 com Endereço
            builder.HasOne(e => e.Endereco)
                .WithOne()
                .HasForeignKey<Empreendimento>(e => e.EnderecoId);
        }
    }
}
