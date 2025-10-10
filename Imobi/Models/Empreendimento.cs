using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Imobi.Models;

#nullable disable
public class Empreendimento
{
    public int Id { get; set; }
    [Display(Name = "Ativo")]
    public bool Status { get; set; }

    [Display(Name = "Empreendimento")]
    public string Nome { get; set; }

    [Display(Name = "Sobre o imóvel")]
    public string Sobre { get; set; }

    [Display(Name = "Área")]
    public string AreaConstruida { get; set; }

    [Display(Name = "Estágio")]
    public string Estagio { get; set; }

    [Display(Name = "Banheiros")]
    public int? BanheirosTotal { get; set; }

    [Display(Name = "Dormitórios")]
    public int? DormitoriosTotal { get; set; }

    [Display(Name = "Suítes")]
    public int? SuitesTotal { get; set; }

    [Display(Name = "Vagas de Garagem")]
    public int? VagasTotal { get; set; }


    public List<Unidade> Unidades { get; set; } = new List<Unidade>();
    public int UnidadeId { get; set; }

    public Endereco Endereco { get; set; } = new Endereco();
    public int EnderecoId { get; set; }

    public List<Arquivo> Arquivos { get; set; }
    public int ArquivoId { get; set; }

    [NotMapped]
    public List<IFormFile> Imagens { get; set; }
}
