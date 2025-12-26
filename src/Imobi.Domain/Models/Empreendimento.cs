using Imobi.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Imobi.Domain.Models
{
    public class Empreendimento : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public string Sobre { get; set; } = string.Empty;

        // Alterado de string para double? (Área geralmente tem casas decimais)
        public double? AreaConstruida { get; set; }

        public EstagioObraEnum Estagio { get; set; }


        // Relacionamentos
        public ICollection<Unidade> Unidades { get; set; } = new List<Unidade>();

        public Guid EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }

        [Obsolete("Use a coleção Imagens agora.")]
        public virtual ICollection<Arquivo> Arquivos { get; set; } = new List<Arquivo>();
        public virtual ICollection<Imagem> Imagens { get; set; } = new List<Imagem>();
    }
}