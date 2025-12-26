using Imobi.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Imobi.MVC.ViewModels
{
    public class EmpreendimentoViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Status")]
        public bool Status { get; set; } = true;

        [Required(ErrorMessage = "O nome é obrigatório")]
        [Display(Name = "Nome do Empreendimento")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Sobre o Empreendimento")]
        public string Sobre { get; set; } = string.Empty;

        [Display(Name = "Estágio da Obra")]
        public EstagioObraEnum Estagio { get; set; }

        // --- RELACIONAMENTOS ---
        public Guid EnderecoId { get; set; }
        public EnderecoViewModel Endereco { get; set; } = new();

        public List<UnidadeViewModel> Unidades { get; set; } = new();

        [Display(Name = "Galeria")]
        public List<ImagemViewModel> Imagens { get; set; } = new List<ImagemViewModel>();

        public List<string> ImagensBase64 { get; set; } = new List<string>();
    }
}