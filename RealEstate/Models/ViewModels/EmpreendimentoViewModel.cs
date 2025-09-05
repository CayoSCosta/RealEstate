using RealEstate.Models.ViewModels;
using System.ComponentModel;

namespace RealEstate.Models.Dtos;
public class EmpreendimentoViewModel : ViewModelBaseId
{
    public string? Nome { get; set; }
    
    [DisplayName("Descrição")]
    public string? Descricao { get; set; }
    
    public string? Status { get; set; }

    [DisplayName("Área min")]
    public int AreaConstruidaMin { get; set; }
    
    [DisplayName("Área max")]
    public int AreaConstruidaMax { get; set; }
    
    [DisplayName("Dormitórios min")]
    public int DormitoriosMin { get; set; }
    
    [DisplayName("Dormitórios max")]
    public int DormitoriosMax { get; set; }
    
    [DisplayName("Banheiros min")]
    public int BanheirosMin { get; set; }
    
    [DisplayName("Banheiros max")]
    public int BanheirosMax { get; set; }

    [DisplayName("Suites min")]
    public int SuitesMin { get; set; }

    [DisplayName("Suites max")]
    public int SuitesMax { get; set; }

    [DisplayName("Vagas de garagem min")]
    public int VagasDeGaragemMin { get; set; }

    [DisplayName("Vagas de garagem min max")]
    public int VagasDeGaragemMax { get; set; }

    public List<UnidadeViewModel> Unidades { get; set; } = new List<UnidadeViewModel>();

    [DisplayName("Endereço")]
    public EnderecoViewModel Endereco { get; set; } = new EnderecoViewModel();

    public List<ImagemViewModel>? Imagens { get; set; } = new List<ImagemViewModel>();

    public List<IFormFile> ArquivoImagens { get; set; } = new List<IFormFile>();
    public List<IFormFile> Arquivos{ get; set; } = new List<IFormFile>();

}