using System.ComponentModel.DataAnnotations.Schema;

namespace Imobi.Models;

#nullable disable
public class Arquivo
{
    public int Id { get; set; }

    public string NomeArquivo { get; set; } = default!;

    public string Caminho { get; set; } = default!;

    public string Extensao { get; set; } = default!;

    public TipoArquivo Tipo { get; set; }

    public int EmpreendimentoId { get; set; }

    [NotMapped]
    public List<IFormFile> Arquivos { get; set; } = new();
}
