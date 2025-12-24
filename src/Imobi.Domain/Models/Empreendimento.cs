using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imobi.Domain.Models
{
    public class Empreendimento : Entity
    {
        public bool Status { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sobre { get; set; } = string.Empty;

        public string AreaConstruida { get; set; } = string.Empty;
        public string Estagio { get; set; } = string.Empty;
        public string BanheirosTotal { get; set; } = string.Empty;
        public string DormitoriosTotal { get; set; } = string.Empty;
        public string SuitesTotal { get; set; } = string.Empty;
        public string VagasTotal { get; set; } = string.Empty;

        // Relacionamentos
        public ICollection<Unidade> Unidades { get; set; } = new List<Unidade>();

        public Guid EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }

        public ICollection<Arquivo> Arquivos { get; set; } = new List<Arquivo>();
    }
}
