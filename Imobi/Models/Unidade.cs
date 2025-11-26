using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Imobi.Models;

public class Unidade
{
    public int Id { get; set; }

    [Display(Name = "Ativo")]
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

    public Empreendimento? Empreendimento { get; set; }

    [Display(Name = "Empreendimento")]
    public int EmpreendimentoId { get; set; }

    public List<Arquivo>? Arquivos { get; set; }
    public int ArquivoId { get; set; }

    [NotMapped]
    public List<IFormFile>? Imagens { get; set; }
}
