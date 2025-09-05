namespace RealEstate.Models.Entities.Empreendimento;

public class Imagem : EntityId
{
    public string? ThumbCaminho { get; set; }
    public string? MediumCaminho { get; set; }
    public string? LargeCaminho { get; set; }
    public string? XLargeCaminho { get; set; }

    public string? ThumbTamanho { get; set; }
    public string? MediumTamanho { get; set; }
    public string? LargeTamanho { get; set; }
    public string? XLargeTamanho { get; set; }
    
    public string? ThumbExtensao { get; set; }
    public string? MediumExtensao { get; set; }
    public string? LargeExtensao { get; set; }
    public string? XLargeExtensao { get; set; }

    public Guid EmpreendimentoId { get; set; }
    public Empreendimento? Empreendimento { get; set; }
}
