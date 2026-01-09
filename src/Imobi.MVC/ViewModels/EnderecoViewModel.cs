using Imobi.MVC.ViewModels.Validator.Base;
using System.ComponentModel.DataAnnotations;

namespace Imobi.MVC.ViewModels
{
    public class EnderecoViewModel
    {
        public Guid Id { get; set; }

        [ValidateAs(ValidationType.CEP)]
        [Display(Name = "CEP")]
        public string Cep { get; set; } = string.Empty;

        [ValidateAs(ValidationType.Text)]
        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; } = string.Empty;

        [ValidateAs(ValidationType.Number)]
        [Display(Name = "Número")]
        public string Numero { get; set; } = string.Empty;

        [ValidateAs(ValidationType.Text)]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; } = string.Empty;

        [ValidateAs(ValidationType.Text)]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; } = string.Empty;

        [ValidateAs(ValidationType.Text)]
        [Display(Name = "UF")]
        public string Uf { get; set; } = string.Empty;

        [ValidateAs(ValidationType.Text)]
        [Display(Name = "Complemento")]
        public string Complemento { get; set; } = string.Empty;
    }
}