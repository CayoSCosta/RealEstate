using Imobi.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Imobi.Domain.Interfaces
{
    public interface IEmpreendimentoService : IDisposable
    {
        Task Adicionar(Empreendimento empreendimento, List<Imagem> imagensDoForm);
        Task Atualizar(Empreendimento empreendimento, List<Imagem> imagensDoForm);
        Task<Empreendimento?> ObterComDetalhes(Guid id);
        Task<Empreendimento?> ObterPorId(Guid id);
        Task<IEnumerable<Empreendimento>> ObterTodos();
        Task Remover(Guid id);
        Task RemoverImagem(Guid imagemId);
    }
}
