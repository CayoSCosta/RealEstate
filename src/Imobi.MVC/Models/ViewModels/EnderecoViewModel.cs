using System.ComponentModel.DataAnnotations;

namespace Imobi.MVC.Models.ViewModels
{
    public class EnderecoViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório")]
        public string Cep { get; set; } = string.Empty;

        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
    }
}
