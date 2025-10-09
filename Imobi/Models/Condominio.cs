namespace Imobi.Models;

public class Condominio
{
    public int Id { get; set; }

    public InstalacaoCondominio Instalacoes { get; set; } = new();

    public Empreendimento? Empreendimento { get; set; }

    public Guid EmpreendimentoId { get; set; }

    public Arquivo? Arquivo { get; set; }
}
