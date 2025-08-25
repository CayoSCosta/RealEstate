namespace RealEstate.Models.Entities.Empreendimento;

public class Empreendimento : EntityBase
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public int AreaConstruidaMin { get; set; }
    public int AreaConstruidaMax { get; set; }
    public int DormitoriosMin { get; set; }
    public int DormitoriosMax { get; set; }
    public int BanheirosMin { get; set; }
    public int BanheirosMax { get; set; }
    public Endereco? Endereco { get; set; }
}
