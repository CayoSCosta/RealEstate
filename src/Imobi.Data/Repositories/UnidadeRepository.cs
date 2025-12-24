using Imobi.Data.Context;
using Imobi.Domain.Interfaces;
using Imobi.Domain.Models;
using Imobi.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Imobi.Data.Repositories
{
    public class UnidadeRepository : Repository<Unidade>, IUnidadeRepository
    {
        public UnidadeRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<Unidade?> ObterPorId(Guid id)
        {
            return await DbSet
                .Include(u => u.Empreendimento)
                .Include(u => u.Arquivos) 
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public override async Task<List<Unidade>> ObterTodos()
        {
            return await DbSet
                .AsNoTracking()
                .Include(u => u.Empreendimento)
                .Include(u => u.Arquivos)
                .ToListAsync();
        }

        public async Task<IEnumerable<Unidade>> BuscarPorEmpreendimentoAsync(Guid empreendimentoId)
        {
            return await DbSet
                .AsNoTracking()
                .Include(u => u.Empreendimento)
                .Include(u => u.Arquivos)
                .Where(u => u.EmpreendimentoId == empreendimentoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Unidade>> BuscarPorFiltrosAsync(int? quartos, decimal? precoMaximo)
        {
            var query = DbSet
                .AsNoTracking()
                .Include(u => u.Empreendimento)
                .Include(u => u.Arquivos)
                .AsQueryable();

            if (quartos.HasValue)
                query = query.Where(u => u.Dormitorios == quartos.Value);

            if (precoMaximo.HasValue)
                query = query.Where(u => u.Valor <= precoMaximo.Value);

            return await query.ToListAsync();
        }
    }
}