using Imobi.MVC.ViewModels.Validator.Base;
using System.ComponentModel.DataAnnotations;

namespace Imobi.MVC.ViewModels
{
    public class UnidadeViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Ativo")]
        public bool Status { get; set; } = true;

        [ValidateAs(ValidationType.Title)]
        [Display(Name = "Tipo da Unidade")]
        public string Tipo { get; set; } = string.Empty;

        [ValidateAs(ValidationType.Number)]
        public int Dormitorios { get; set; }

        [ValidateAs(ValidationType.Number)]
        public int Suites { get; set; }

        [ValidateAs(ValidationType.Number)]
        public int Banheiros { get; set; }

        [ValidateAs(ValidationType.Number)]
        public int Vagas { get; set; }

        [ValidateAs(ValidationType.Number)]
        [Display(Name = "Área Privativa (m²)")]
        public int AreaConstruida { get; set; }

        [ValidateAs(ValidationType.Currency)]
        [Display(Name = "Valor de Venda")]
        public decimal? Valor { get; set; }

        public DateTime CriadoEm { get; set; }

        public Guid EmpreendimentoId { get; set; }

        public EmpreendimentoViewModel? Empreendimento { get; set; }

        public List<ImagemViewModel> Imagens { get; set; } = new List<ImagemViewModel>();

        public List<string> ImagensBase64 { get; set; } = new List<string>();
    }
}