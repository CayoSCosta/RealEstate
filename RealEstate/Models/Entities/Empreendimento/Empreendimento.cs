namespace RealEstate.Models.Entities.Empreendimento;

public class Empreendimento : EntityBase
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public string? Status { get; set; }
    public int AreaConstruidaMin { get; set; }
    public int AreaConstruidaMax { get; set; }
    public int DormitoriosMin { get; set; }
    public int DormitoriosMax { get; set; }
    public int BanheirosMin { get; set; }
    public int BanheirosMax { get; set; }
    public int SuitesMin { get; set; }
    public int SuitesMax { get; set; }
    public int VagasDeGaragemMin { get; set; }
    public int VagasDeGaragemMax { get; set; }
    public Endereco? Endereco { get; set; }
    public List<Unidade>? Unidades { get; set; }
    public List<Arquivo>? Imagens { get; set; }
    public List<Arquivo>? Arquivos { get; set; }

}
