using Microsoft.AspNetCore.Identity;

namespace Imobi.Models.Identity;

public class ApplicationUser : IdentityUser
{
    public string Nome { get; set; } = string.Empty;
    public string SobreNome { get; set; } = string.Empty;
    public string? FotoUrl { get; set; }
    public bool Ativo { get; set; } = false;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime ModificadoEm { get; set; } = DateTime.UtcNow;
    public DateTime? UltimoLogin { get; set; }
}

