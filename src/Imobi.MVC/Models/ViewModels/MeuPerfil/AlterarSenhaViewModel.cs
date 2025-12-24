using System.ComponentModel.DataAnnotations;

namespace Imobi.MVC.Models.ViewModels.MeuPerfil
{
    public class AlterarSenhaViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        public string SenhaAtual { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NovaSenha { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NovaSenha))]
        [Display(Name = "Confirmar nova senha")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
