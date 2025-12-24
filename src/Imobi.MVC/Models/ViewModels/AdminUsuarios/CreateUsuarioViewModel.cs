using Imobi.Models.ViewModels.AdminUsuarios;
using System.ComponentModel.DataAnnotations;

namespace Imobi.MVC.Models.ViewModels.AdminUsuarios
{

    public class CreateUsuarioViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string SobreNome { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;

        public bool Ativo { get; set; } = true;

        public List<RoleSelection>? Roles { get; set; }
    }
}
