namespace RealEstate.Models.Entities.Empreendimento;

public class Imagem : EntityId
{
    public string? Nomedoarquivo { get; set; }
    public string? Caminho { get; set; }
    public string? Extensao { get; set; }
    public long Tamanho { get; set; }
    public Guid EmpreendimentoId { get; set; }
    public Empreendimento? Empreendimento { get; set; }
}
