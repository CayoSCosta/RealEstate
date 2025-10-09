using System.ComponentModel.DataAnnotations;

namespace Imobi.Models;

public class InstalacaoCondominio
{
    public int Id { get; set; }

    [Display(Name = "Recursos")]
    public List<string>? Recursos { get; set; }
}
