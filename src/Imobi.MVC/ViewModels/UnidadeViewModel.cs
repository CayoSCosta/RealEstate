using System.ComponentModel.DataAnnotations;

namespace Imobi.MVC.ViewModels
{
    public class UnidadeViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Ativo")]
        public bool Status { get; set; } = true;

        [Required(ErrorMessage = "O tipo é obrigatório (Ex: Apartamento, Studio)")]
        [Display(Name = "Tipo da Unidade")]
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
        [Display(Name = "Área Privativa (m²)")]
        public int AreaConstruida { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Valor de Venda")]
        public decimal? Valor { get; set; }

        [Display(Name = "Registro")]
        public DateTime CriadoEm { get; set; }

        [Required]
        public Guid EmpreendimentoId { get; set; }

        public EmpreendimentoViewModel? Empreendimento { get; set; }

        [Display(Name = "Galeria")]
        public List<ImagemViewModel> Imagens { get; set; } = new List<ImagemViewModel>();

        public List<string> ImagensBase64 { get; set; } = new List<string>();
    }
}