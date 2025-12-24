using Microsoft.AspNetCore.Identity;

namespace Imobi.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; } = string.Empty;
        public string SobreNome { get; set; } = string.Empty;
        public string? FotoUrl { get; set; }
        public bool Ativo { get; set; } = true;
        public string? EmailSecundario { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public DateTime ModificadoEm { get; set; } = DateTime.UtcNow;
        public DateTime? UltimoLogin { get; set; }
    }
}
