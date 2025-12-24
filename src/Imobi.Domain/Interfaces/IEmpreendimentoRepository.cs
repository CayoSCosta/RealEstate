using Imobi.Domain.Models;

namespace Imobi.Domain.Interfaces
{
    public interface IEmpreendimentoRepository : IRepository<Empreendimento>
    {
        Task<Empreendimento?> ObterComDetalhesAsync(Guid id);
    }
}
