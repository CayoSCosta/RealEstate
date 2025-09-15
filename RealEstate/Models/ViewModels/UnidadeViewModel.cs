using System.ComponentModel;

namespace RealEstate.Models.ViewModels;

public class UnidadeViewModel : ViewModelBaseId
{
    public string? Tipo { get; set; }
    [DisplayName("Dormitórios")]
    public int Dormitorios { get; set; }

    public int Banheiros { get; set; }

    [DisplayName("Suítes")]
    public int Suites { get; set; }

    [DisplayName("Vagas de garagem")]
    public int VagasDeGaragem { get; set; }

    [DisplayName("Área construída")]
    public int AreaConstruida { get; set; }
    public decimal Valor { get; set; }
    public string? Status { get; set; }
    public bool Ativo { get; set; }

    public Guid EmpreendimentoId { get; set; }
    public EmpreendimentoViewModel? Empreendimento { get; set; }
    public List<ImagemViewModel>? Imagens { get; set; } = new List<ImagemViewModel>();
    public List<IFormFile> Plantas { get; set; } = new List<IFormFile>();
}
