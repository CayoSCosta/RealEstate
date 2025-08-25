namespace RealEstate.Models.Entities.Empreendimento;

public class Unidade : EntityId
{
    public string? Tipo { get; set; }
    public int Dormitorios { get; set; }
    public int Suites { get; set; }
    public int Banheiros { get; set; }
    public int Vagas { get; set; }

    public List<Planta>? Plantas { get; set; }
}
