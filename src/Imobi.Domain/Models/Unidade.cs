using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imobi.Domain.Models
{
    public class Unidade : Entity
    {
        public string? Tipo { get; set; }
        public int Dormitorios { get; set; }
        public int Suites { get; set; }
        public int Banheiros { get; set; }
        public int Vagas { get; set; }
        public int AreaConstruida { get; set; }
        public decimal? Valor { get; set; }

        public Guid EmpreendimentoId { get; set; }
        public Empreendimento? Empreendimento { get; set; }


        [Obsolete("Use a coleção Imagens agora.")]
        public virtual ICollection<Arquivo> Arquivos { get; set; } = new List<Arquivo>();
        public virtual ICollection<Imagem> Imagens { get; set; } = new List<Imagem>();
    }
}
