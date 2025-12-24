using Imobi.Data.Context;
using Imobi.Domain.Interfaces;
using Imobi.Domain.Models;
using Imobi.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Imobi.Data.Repositories
{
    public class EmpreendimentoRepository : Repository<Empreendimento>, IEmpreendimentoRepository
    {
        public EmpreendimentoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Empreendimento?> ObterComDetalhesAsync(Guid id)
        {
            return await Db.Empreendimentos
                .AsNoTracking()
                .Include(e => e.Endereco)
                .Include(e => e.Unidades)
                .Include(e => e.Arquivos)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Empreendimento>> ObterTodos()
        {
            return await Db.Empreendimentos
                .AsNoTracking()
                .Include(e => e.Endereco)
                .ToListAsync();
        }
    }
}
