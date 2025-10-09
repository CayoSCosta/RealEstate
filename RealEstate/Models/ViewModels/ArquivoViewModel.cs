using RealEstate.Models.Entities.Empreendimento;

namespace RealEstate.Models.ViewModels;

public class ArquivoViewModel
{
    public IFormFile? Arquivo { get; set; }

    public Guid? Id { get; set; }
    public string NomeArquivo { get; set; } = default!;
    public string Caminho { get; set; } = default!;
    public string Extensao { get; set; } = default!;
    public TipoArquivo Tipo { get; set; } = new();
    public Guid? EmpreendimentoId { get; set; }
    public Guid? UnidadeId { get; set; }
}

//public class ArquivoViewModel
//{
//    public Guid? Id { get; set; }
//    public string NomeArquivo { get; set; } = default!;
//    public string Caminho { get; set; } = default!;
//    public string Extensao { get; set; } = default!;
//    public TipoArquivo Tipo { get; set; }
//    public Guid? EmpreendimentoId { get; set; }
//    public Guid? UnidadeId { get; set; }

//    // Usado no upload
//    public IFormFile? Arquivo { get; set; }
//}