namespace Imobi.MVC.Models.ViewModels.MeuPerfil;

public class PreferenciasViewModel
{
    // Conta
    public string Email { get; set; } = string.Empty;
    public string? EmailSecundario { get; set; }
    public bool Ativo { get; set; }

    public DateTime DataCriacao { get; set; }
    public DateTime? UltimoLogin { get; set; }

    // Preferências
    public string Tema { get; set; } = "Claro";
    public string Idioma { get; set; } = "pt-BR";
    public bool ReceberEmails { get; set; }

    // Controle visual
    public bool PodeAlterarEmail { get; set; } = true;
}
