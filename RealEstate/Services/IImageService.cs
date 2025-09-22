using RealEstate.Models;
using RealEstate.Models.Entities.Empreendimento;

namespace RealEstate.Services;

public interface IImagemService
{
    Task<Imagem> ProcessarImagemAsync(IFormFile file, TipoImagem tipo, string nomeEmpreendimento, Guid? empreendimentoId = null, Guid? unidadeId = null);
}