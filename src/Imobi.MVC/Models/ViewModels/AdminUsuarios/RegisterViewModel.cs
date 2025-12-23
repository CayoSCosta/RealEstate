using System.ComponentModel.DataAnnotations;

namespace Imobi.Models.ViewModels.AdminUsuarios
{
    public class RegisterViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Nome { get; set; } = "";

        [Required]
        public string SobreNome { get; set; } = "";

        [Required, DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; } = "";

        // opcional: para redirecionamento pós-criação
        public string? ReturnUrl { get; set; }
    }
}
