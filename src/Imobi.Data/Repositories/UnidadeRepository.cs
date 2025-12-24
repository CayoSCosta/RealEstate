using Imobi.Data.Context;
using Imobi.Domain.Interfaces;
using Imobi.Domain.Models;
using Imobi.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imobi.Data.Repositories
{
    public class UnidadeRepository : Repository<Unidade>, IUnidadeRepository
    {
        public UnidadeRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Unidade>> BuscarPorEmpreendimentoAsync(Guid empreendimentoId)
        {
            return await DbSet
                .Where(u => u.EmpreendimentoId == empreendimentoId)
                .Include(u => u.Arquivos)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Unidade>> BuscarPorFiltrosAsync(int? quartos, decimal? precoMaximo)
        {
            var query = DbSet.AsNoTracking().AsQueryable();

            if (quartos.HasValue)
                query = query.Where(u => u.Dormitorios == quartos.Value);

            if (precoMaximo.HasValue)
                query = query.Where(u => u.Valor <= precoMaximo.Value);

            return await query.ToListAsync();
        }
    }
}
