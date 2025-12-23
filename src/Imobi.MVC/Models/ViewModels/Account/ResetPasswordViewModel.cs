using System.ComponentModel.DataAnnotations;

namespace Imobi.ViewModels.Account;

public class ResetPasswordViewModel
{
    [Required, EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Token { get; set; }

    [Required, DataType(DataType.Password)]
    [MinLength(6)]
    public string? Password { get; set; }

    [Required, DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "As senhas não conferem.")]
    public string? ConfirmPassword { get; set; }
}
