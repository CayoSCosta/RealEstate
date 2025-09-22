using System.ComponentModel;

namespace RealEstate.Models.ViewModels;

public class EmpreendimentoViewModel : ViewModelBaseId
{
    [DisplayName("Empreendimento")]
    public string? Nome { get; set; }

    [DisplayName("Descrição")]
    public string? Descricao { get; set; }

    public bool Ativo { get; set; }

    public string? Status { get; set; }

    [DisplayName("Área min")]
    public int? AreaConstruidaMin { get; set; }

    [DisplayName("Área max")]
    public int? AreaConstruidaMax { get; set; }

    [DisplayName("Dormitórios min")]
    public int? DormitoriosMin { get; set; }

    [DisplayName("Dormitórios max")]
    public int? DormitoriosMax { get; set; }

    [DisplayName("Banheiros min")]
    public int? BanheirosMin { get; set; }

    [DisplayName("Banheiros max")]
    public int? BanheirosMax { get; set; }

    [DisplayName("Suítes min")]
    public int? SuitesMin { get; set; }

    [DisplayName("Suítes max")]
    public int? SuitesMax { get; set; }

    [DisplayName("Vagas de garagem min")]
    public int? VagasDeGaragemMin { get; set; }

    [DisplayName("Vagas de garagem max")]
    public int? VagasDeGaragemMax { get; set; }

    public List<UnidadeViewModel>? Unidades { get; set; } = new();

    [DisplayName("Endereço")]
    public EnderecoViewModel? Endereco { get; set; } = new();

    // Novo modelo de imagem
    public List<ImagemViewModel>? Imagens { get; set; } = new();

    // Upload de arquivos diversos (pdf, txt, etc.)
    public List<IFormFile>? Arquivos { get; set; } = new();
}
