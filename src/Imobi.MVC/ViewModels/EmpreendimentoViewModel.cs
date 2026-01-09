using Imobi.Domain.Enum;
using Imobi.MVC.ViewModels.Validator.Base;
using System.ComponentModel.DataAnnotations;

namespace Imobi.MVC.ViewModels
{
    public class EmpreendimentoViewModel
    {
        public Guid Id { get; set; }

        public bool Status { get; set; } = true;

        [ValidateAs(ValidationType.Title)]
        [Display(Name = "Nome do Empreendimento")]
        public string Nome { get; set; } = string.Empty;

        [ValidateAs(ValidationType.Text)]
        [Display(Name = "Sobre o Empreendimento")]
        public string Sobre { get; set; } = string.Empty;

        public DateTime CriadoEm { get; set; }

        public EstagioObraEnum Estagio { get; set; }

        public Guid EnderecoId { get; set; }
        public EnderecoViewModel Endereco { get; set; } = new();

        public List<UnidadeViewModel> Unidades { get; set; } = new();

        public List<ImagemViewModel> Imagens { get; set; } = new List<ImagemViewModel>();

        public List<string> ImagensBase64 { get; set; } = new List<string>();
    }
}