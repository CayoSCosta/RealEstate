using Imobi.Domain.Models;
using Imobi.Domain.Models.Util;
using Microsoft.AspNetCore.Http;

namespace Imobi.Application.Services
{
    public interface IUnidadeService : IDisposable
    {
        Task<IEnumerable<Unidade>> ObterTodos();
        Task<Unidade?> ObterPorId(Guid id);
        Task Adicionar(Unidade unidade, List<IFormFile> imagens, string webRootPath);
        Task Atualizar(Unidade unidade, List<IFormFile> imagens, string webRootPath);
        Task Remover(Guid id);
        Task<PagedResult<Unidade>> ObterPaginado(SearchParametersDomain parameters);
    }
}