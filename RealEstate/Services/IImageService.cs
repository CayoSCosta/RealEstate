using RealEstate.Models;
using RealEstate.Models.Entities.Empreendimento;

namespace RealEstate.Services;

public interface IImagemService
{
    Task<Imagem> SalvarImagemAsync(
        IFormFile arquivo,
        TipoImagem tipo,
        Guid? unidadeId = null,
        Guid? empreendimentoId = null);
}