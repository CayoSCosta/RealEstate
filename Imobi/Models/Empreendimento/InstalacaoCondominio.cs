using System.ComponentModel.DataAnnotations;

namespace Imobi.Models.Empreendimento;

public class InstalacaoCondominio
{
    public int Id { get; set; }

    [Display(Name = "Recursos")]
    public List<string>? Recursos { get; set; }
}
