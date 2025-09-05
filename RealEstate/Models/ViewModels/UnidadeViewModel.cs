using RealEstate.Models.Dtos;

namespace RealEstate.Models.ViewModels;

public class UnidadeViewModel : ViewModelBaseId
{
    public int Quartos { get; set; }
    public int Banheiros { get; set; }
    public int Suites { get; set; }
    public int VagasDeGaragem { get; set; }
    public int AreaConstruida { get; set; }
    public decimal Valor { get; set; }
    public string? Status { get; set; }

    public List<IFormFile> Plantas { get; set; } = new List<IFormFile>();
}
