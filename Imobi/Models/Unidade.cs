using System.ComponentModel.DataAnnotations;

namespace Imobi.Models;

public class Unidade
{
    public int Id { get; set; }
    public bool Status { get; set; }

    [Display(Name = "Tipo")]
    public string? Tipo { get; set; }

    [Display(Name = "Dormitórios")]
    public int Dormitorios { get; set; }

    [Display(Name = "Suítes")]
    public int Suites { get; set; }

    [Display(Name = "Banheiros")]
    public int Banheiros { get; set; }

    [Display(Name = "Vagas de Garagem")]
    public int Vagas { get; set; }

    [Display(Name = "Área Construída (m²)")]
    public int AreaConstruida { get; set; }

    [Display(Name = "Valor (R$)")]
    public decimal? Valor { get; set; }

    public Empreendimento Empreendimento { get; set; } = new Empreendimento();
    public int EmpreendimentoId { get; set; }

}
