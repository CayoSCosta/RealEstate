using Imobi.Data.Context;
using Imobi.Domain.Interfaces;
using Imobi.Domain.Models;
using Imobi.Infra.Data.Repositories;

namespace Imobi.Data.Repositories
{
    public class ImagemRepository : Repository<Imagem>, IImagemRepository
    {
        public ImagemRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
