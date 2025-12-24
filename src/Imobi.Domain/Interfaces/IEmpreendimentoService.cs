using Imobi.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Imobi.Domain.Interfaces
{
    public interface IEmpreendimentoService : IDisposable
    {
        Task<IEnumerable<Empreendimento>> ObterTodos();
        Task<Empreendimento?> ObterPorId(Guid id);
        Task<Empreendimento?> ObterComDetalhes(Guid id);

        Task Adicionar(Empreendimento empreendimento, List<IFormFile> imagens, string webRootPath);
        Task Atualizar(Empreendimento empreendimento, List<IFormFile> imagens, string webRootPath);
        Task Remover(Guid id, string webRootPath);

        Task RemoverImagem(Guid arquivoId, string webRootPath);
    }
}
