namespace RealEstate.Models.Entities.Empreendimento;

public class Imagem : EntityId
{
    public Guid? EmpreendimentoId { get; set; }
    public Empreendimento Empreendimento { get; set; } = default!;

    public Guid? UnidadeId { get; set; }
    public Unidade? Unidade { get; set; }

    public string NomeArquivo { get; set; } = default!;  // ex: "fachada-1.jpg"
    public string Caminho { get; set; } = default!;      // ex: "/uploads/empreendimentoX/..."
    public string Extensao { get; set; } = default!;     // ex: ".jpg"
    public TipoImagem Tipo { get; set; }                 // Fachada, Planta, Diversa...

    public List<ImagemVersao> Versoes { get; set; } = new(); // Lista de diferentes tamanhos
}

public class ImagemVersao
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = default!;       // ex: "thumb", "medium", "large"
    public string Caminho { get; set; } = default!;    // caminho físico ou URL
    public string Extensao { get; set; } = default!;   // ".jpg"
    public string? Tamanho { get; set; }               // "150x150", "600x400", etc.
}
