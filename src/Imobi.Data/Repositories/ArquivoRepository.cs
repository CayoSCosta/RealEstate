using Imobi.Data.Context;
using Imobi.Domain.Interfaces;
using Imobi.Domain.Models;
using Imobi.Infra.Data.Repositories;

namespace Imobi.Data.Repositories
{
    public class ArquivoRepository : Repository<Arquivo>, IArquivoRepository
    {
        public ArquivoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
