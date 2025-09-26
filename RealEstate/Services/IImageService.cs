using RealEstate.Models;
using RealEstate.Models.Entities.Empreendimento;

namespace RealEstate.Services;

public interface IImagemService
{
    Task<Arquivo> ProcessarImagemAsync(IFormFile file, string nomeEntidade, TipoArquivo tipoArquivo, TipoEntidade tipoEntidade, Guid? entidadeId = null);
}