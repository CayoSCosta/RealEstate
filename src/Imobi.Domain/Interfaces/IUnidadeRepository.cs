using Imobi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imobi.Domain.Interfaces
{
    public interface IUnidadeRepository : IRepository<Unidade>
    {
        Task<IEnumerable<Unidade>> BuscarPorEmpreendimentoAsync(Guid empreendimentoId);
        Task<IEnumerable<Unidade>> BuscarPorFiltrosAsync(int? quartos, decimal? precoMaximo);
    }
}
