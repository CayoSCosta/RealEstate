using Imobi.Domain.Models;

namespace Imobi.Domain.Interfaces
{
    public interface IEnderecoService : IDisposable
    {
        Task<Endereco?> BuscarEnderecoPorCep(string cep);
        Task<Endereco?> ObterPorId(Guid id);
        Task Atualizar(Endereco endereco);
    }
}
