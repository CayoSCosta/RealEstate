namespace Imobi.Models.ViewModels.AdminUsuarios;

#nullable disable
public class UsuarioListItemViewModel
{
    public string Id { get; set; }
    public string Email { get; set; }
    public bool Ativo { get; set; }
    public List<string> Roles { get; set; } = new();
}
