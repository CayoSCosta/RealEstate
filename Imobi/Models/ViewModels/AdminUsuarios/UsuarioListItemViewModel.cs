using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Imobi.Models.ViewModels.AdminUsuarios;

#nullable disable
public class UsuarioListItemViewModel
{
    public string Id { get; set; }
    public string Nome { get; set; }
    [Display(Name = "Sobrenome")]
    public string SobreNome { get; set; }
    public string Email { get; set; }
    public bool Ativo { get; set; }
    public List<string> RolesNames { get; set; } = new();

    public List<RoleSelection> Roles { get; set; } = new();
}
