using System.ComponentModel.DataAnnotations;

namespace Imobi.ViewModels.Account;

public class RegisterViewModel
{
    [Required]
    public string Nome { get; set; } = string.Empty;

    [Required]
    public string SobreNome { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, DataType(DataType.Password)]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    [Required, DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "As senhas não conferem.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string? ReturnUrl { get; set; }
}
