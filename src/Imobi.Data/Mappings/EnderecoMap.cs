using Imobi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imobi.Data.Mappings
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Enderecos", "realestate");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Cep).HasMaxLength(8).IsRequired();
            builder.Property(e => e.Logradouro).HasMaxLength(200);
        }
    }
}
