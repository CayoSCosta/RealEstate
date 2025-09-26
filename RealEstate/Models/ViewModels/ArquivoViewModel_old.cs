using RealEstate.Models.Entities.Empreendimento;

namespace RealEstate.Models.ViewModels;

public class ArquivoViewModel_old
{
    public string? Nomedoarquivo { get; set; }
    public string? Caminho { get; set; }
    public string? Extensao { get; set; }
    public long TamanhoDoArquivo { get; set; }
    public Guid EmpreendimentoId { get; set; }
    public Empreendimento? Empreendimento { get; set; }
}
