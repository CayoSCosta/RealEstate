using Imobi.Data.Context;
using Imobi.Domain.Interfaces;
using Imobi.Domain.Models;
using Imobi.Domain.Models.Util;
using Imobi.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Imobi.Infra.Data.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
{
    protected readonly ApplicationDbContext Db;
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(ApplicationDbContext db)
    {
        Db = db;
        DbSet = db.Set<TEntity>();
    }

    public virtual async Task<PagedResult<TEntity>> ObterPaginado(
        SearchParametersDomain parameters,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = DbSet.AsNoTracking();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        query = query.ApplyFilters(parameters);

        var totalItems = await query.CountAsync();

        var items = await query
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        return new PagedResult<TEntity>
        {
            Items = items,
            TotalCount = totalItems,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }

    public virtual async Task<TEntity?> ObterPorId(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<List<TEntity>> ObterTodos()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public virtual async Task Adicionar(TEntity entity)
    {
        DbSet.Add(entity);
        await SaveChanges();
    }

    public virtual async Task Atualizar(TEntity entity)
    {
        DbSet.Update(entity);
        await SaveChanges();
    }

    public virtual async Task Remover(Guid id)
    {
        var entity = new TEntity { Id = id };
        DbSet.Remove(entity);
        await SaveChanges();
    }

    public async Task<int> SaveChanges()
    {
        return await Db.SaveChangesAsync();
    }

    public void Dispose()
    {
        Db?.Dispose();
    }
}