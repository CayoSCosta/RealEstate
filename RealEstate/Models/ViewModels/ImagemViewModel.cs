using RealEstate.Models.Entities.Empreendimento;

namespace RealEstate.Models.ViewModels;

public class ImagemViewModel : ViewModelBaseId
{
    public TipoImagem Tipo { get; set; }
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

    public Guid? EmpreendimentoId { get; set; }
    public Guid? UnidadeId { get; set; }
    public EmpreendimentoViewModel? Empreendimento { get; set; }
    public UnidadeViewModel? Unidade { get; set; }
}
