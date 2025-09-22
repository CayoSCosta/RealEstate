using RealEstate.Models.Entities.Empreendimento;

namespace RealEstate.Models.ViewModels;

public class ImagemViewModel
{
    public Guid? Id { get; set; }

    public TipoImagem Tipo { get; set; } = new();
    public string NomeArquivo { get; set; } = default!;
    public string Caminho { get; set; } = default!;
    public string Extensao { get; set; } = default!;

    public List<ImagemVersaoViewModel> Versoes { get; set; } = new();

    // Upload
    public IFormFile? Arquivo { get; set; }

    public Guid? EmpreendimentoId { get; set; }
    public Guid? UnidadeId { get; set; }
}

public class ImagemVersaoViewModel
{
    public string Nome { get; set; } = default!;      // "thumb", "medium", "large"
    public string Caminho { get; set; } = default!;
    public string Extensao { get; set; } = default!;
    public string? Tamanho { get; set; }
}
