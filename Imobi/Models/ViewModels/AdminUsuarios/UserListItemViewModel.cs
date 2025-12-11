namespace Imobi.Models.ViewModels.AdminUsuarios;

public class UserListItemViewModel
{
    public string Id { get; set; } = "";
    public string Nome { get; set; } = "";
    public string SobreNome { get; set; } = "";
    public string Email { get; set; } = "";
    public bool Ativo { get; set; } = true;
    public string RolesSummary { get; set; } = ""; // ex: "Admin, User"
}
