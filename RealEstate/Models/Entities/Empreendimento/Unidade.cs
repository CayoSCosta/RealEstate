namespace RealEstate.Models.Entities.Empreendimento;

public class Unidade : EntityBase
{
    public string? Tipo { get; set; }
    public int Dormitorios { get; set; }
    public int Suites { get; set; }
    public int Banheiros { get; set; }
    public int Vagas { get; set; }
    public int AreaConstruida { get; set; }
    public decimal? Valor { get; set; }
    public string? Status { get; set; }

    public Guid? EmpreendimentoId { get; set; }
    public Empreendimento? Empreendimento { get; set; }
    public List<Arquivo>? Imagens { get; set; }
}
