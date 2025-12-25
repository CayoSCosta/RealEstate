using System.ComponentModel.DataAnnotations.Schema;

namespace Imobi.Domain.Models
{
    public class Imagem : Entity
    {
        public string Base64 { get; set; } = string.Empty;

        public string? Legenda { get; set; }

        public int Ordem { get; set; } = 0;

        public string? Tipo { get; set; }

        public Guid? EmpreendimentoId { get; set; }
        [ForeignKey("EmpreendimentoId")]
        public virtual Empreendimento? Empreendimento { get; set; }

        public Guid? UnidadeId { get; set; }
        [ForeignKey("UnidadeId")]
        public virtual Unidade? Unidade { get; set; }
    }
}
