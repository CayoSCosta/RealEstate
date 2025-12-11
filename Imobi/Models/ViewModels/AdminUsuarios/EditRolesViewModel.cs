namespace Imobi.Models.ViewModels.AdminUsuarios;

#nullable disable
public class EditRolesViewModel
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public List<RoleSelection> Roles { get; set; } = new();
}

public class RoleSelection
{
    public string Name { get; set; }
    public bool Selected { get; set; }
}
