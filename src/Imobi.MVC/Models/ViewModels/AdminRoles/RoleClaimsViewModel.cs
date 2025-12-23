namespace Imobi.Models.ViewModels.AdminRoles;

#nullable disable
public class RoleClaimsViewModel
{
    public string RoleId { get; set; }
    public string RoleName { get; set; }

    public List<string> Claims { get; set; } = new();
    public string NewClaim { get; set; }
}
