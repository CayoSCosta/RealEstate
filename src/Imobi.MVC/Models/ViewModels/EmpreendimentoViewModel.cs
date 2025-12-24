using System.ComponentModel.DataAnnotations;

namespace Imobi.MVC.Models.ViewModels
{
    public class EmpreendimentoViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Ativo")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [Display(Name = "Nome do Empreendimento")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Sobre o imóvel")]
        public string Sobre { get; set; } = string.Empty;

        [Display(Name = "Área")]
        public string AreaConstruida { get; set; } = string.Empty;

        [Display(Name = "Estágio")]
        public string Estagio { get; set; } = string.Empty;

        public string BanheirosTotal { get; set; } = string.Empty;
        public string DormitoriosTotal { get; set; } = string.Empty;
        public string SuitesTotal { get; set; } = string.Empty;
        public string VagasTotal { get; set; } = string.Empty;

        public EnderecoViewModel Endereco { get; set; } = new();

        [Display(Name = "Imagens do Empreendimento")]
        public List<IFormFile> ImagensUpload { get; set; } = new();
        public List<ArquivoViewModel> Arquivos { get; set; } = new();
    }
}
