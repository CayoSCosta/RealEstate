using Imobi.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imobi.Domain.Models
{

    public class Arquivo : Entity
    {
        public string NomeArquivo { get; set; } = string.Empty;
        public string Caminho { get; set; } = string.Empty;
        public string Extensao { get; set; } = string.Empty;
        public TipoArquivoEnum Tipo { get; set; }
        public string Descricao { get; set; } = string.Empty;


        // Relacionamentos
        public Guid? EmpreendimentoId { get; set; }
        public Guid? UnidadeId { get; set; }
    }
}
