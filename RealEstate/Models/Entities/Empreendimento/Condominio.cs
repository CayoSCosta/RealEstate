namespace RealEstate.Models.Entities.Empreendimento;

public class Condominio : EntityId
{
    public List<string> Instalacoes { get; set; } = new List<string>();
    public Empreendimento? Empreendimento { get; set; }
    public Guid EmpreendimentoId { get; set; }
}
