namespace RealEstate.Models.Entities.Empreendimento;

public class InstalacaoCondominio : EntityId
{
    public string? Nome { get; set; }
    public List<Condominio> Condominios { get; set; } = new();
}
