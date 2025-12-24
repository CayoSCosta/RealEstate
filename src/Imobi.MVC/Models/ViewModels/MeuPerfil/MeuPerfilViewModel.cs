using System.ComponentModel.DataAnnotations;

namespace Imobi.MVC.Models.ViewModels.MeuPerfil
{
    public class MeuPerfilViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O sobrenome é obrigatório")]
        [Display(Name = "Sobrenome")]
        public string SobreNome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Display(Name = "E-mail Principal")]
        public string Email { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Display(Name = "E-mail Secundário")]
        public string? EmailSecundario { get; set; }

        public string? FotoBase64 { get; set; }

        public string? FotoAtual { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public string? SenhaAtual { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string? NovaSenha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nova Senha")]
        [Compare("NovaSenha", ErrorMessage = "As senhas não conferem.")]
        public string? ConfirmarNovaSenha { get; set; }
    }
}
