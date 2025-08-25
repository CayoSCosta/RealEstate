namespace RealEstate.Models.Entities.Empreendimento;

public class Condominio : EntityId
{
    public List<InstalacaoCondominio> Instalacoes { get; set; } = new();
    public Empreendimento? Empreendimento { get; set; }
    public Guid EmpreendimentoId { get; set; }
}
