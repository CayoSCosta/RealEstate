namespace RealEstate.Models.Entities.Empreendimento;

public class Arquivo : EntityId
{
    public Guid? EntidadeId { get; set; }

    public TipoEntidade TipoEntidade { get; set; }

    public string? NomeArquivo { get; set; } = default!; 
    
    public string? Caminho { get; set; } = default!;   
    
    public string? Extensao { get; set; } = default!; 
    
    public TipoArquivo Tipo { get; set; }                
}
