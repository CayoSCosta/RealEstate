using System.ComponentModel.DataAnnotations;

namespace Imobi.Models;

#nullable disable
public class Endereco
{
    public int Id { get; set; }

    [RegularExpression(@"^\d{8}$", ErrorMessage = "CEP inválido.")]
    public string Cep { get; set; }

    [RegularExpression(@"^\d+$", ErrorMessage = "Número deve ser apenas números.")]
    public string Numero { get; set; }
    public string Logradouro { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Uf { get; set; }

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
