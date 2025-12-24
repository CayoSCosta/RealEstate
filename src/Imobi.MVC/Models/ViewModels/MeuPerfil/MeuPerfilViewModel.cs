using System.ComponentModel.DataAnnotations;

namespace Imobi.MVC.Models.ViewModels.MeuPerfil;

public class MeuPerfilViewModel
{
    [Required]
    [Display(Name = "Nome")]
    public string Nome { get; set; }

    [Required]
    [Display(Name = "Sobrenome")]
    public string SobreNome { get; set; }

    [Required, EmailAddress]
    [Display(Name = "E-mail principal")]
    public string Email { get; set; }

    [EmailAddress]
    [Display(Name = "E-mail secundário")]
    public string? EmailSecundario { get; set; }

    public string? FotoUrl { get; set; }

    public IFormFile? Foto { get; set; }
}
