using System.ComponentModel.DataAnnotations;

namespace Imobi.MVC.ViewModels
{
    public class UnidadeViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Ativo")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "O tipo é obrigatório")]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        [Range(0, 100)]
        public int Dormitorios { get; set; }

        [Required]
        [Range(0, 100)]
        public int Suites { get; set; }

        [Required]
        [Range(0, 100)]
        public int Banheiros { get; set; }

        [Required]
        [Range(0, 100)]
        public int Vagas { get; set; }

        [Required]
        [Display(Name = "Área (m²)")]
        public int AreaConstruida { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Valor { get; set; }

        [Required]
        public Guid EmpreendimentoId { get; set; }

        public EmpreendimentoViewModel Empreendimento { get; set; }

        [Display(Name = "Imagens da Unidade")]
        public List<IFormFile> ImagensUpload { get; set; } = new();
        public List<ArquivoViewModel> Arquivos { get; set; } = new();

        public IEnumerable<ImagemViewModel> Imagens { get; set; } = new List<ImagemViewModel>();
    }
}
