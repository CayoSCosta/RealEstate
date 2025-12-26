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
            var empreendimento = await Db.Empreendimentos
                .Include(e => e.Endereco)
                .Include(e => e.Arquivos)
                .Include(e => e.Imagens)
                .Include(e => e.Unidades)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (empreendimento != null && empreendimento.Imagens.Any())
            {
                empreendimento.Imagens = empreendimento.Imagens
                    .OrderBy(i => i.Ordem)
                    .ToList();
            }

            return empreendimento;
        }

        public async Task<IEnumerable<Empreendimento>> ObterTodos()
        {
            return await Db.Empreendimentos
                .AsNoTracking()
                .Include(e => e.Endereco)
                .ToListAsync();
        }
        public async Task Remover(Guid id)
        {
            var empreendimento = await Db.Empreendimentos
                .Include(e => e.Arquivos)
                .Include(e => e.Imagens)
                .Include(e => e.Unidades)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (empreendimento != null)
            {
                if (empreendimento.Arquivos.Any())
                    Db.Arquivos.RemoveRange(empreendimento.Arquivos);
                if (empreendimento.Imagens.Any())
                    Db.Imagens.RemoveRange(empreendimento.Imagens);

                Db.Empreendimentos.Remove(empreendimento);
            }
            await Db.SaveChangesAsync();
        }
    }
}
