

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MyEducationCenter.DataLayer;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext dbContext;

    public GenericRepository(
        AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public T Create(T entity)
    {
        var stateBeforeAdd = dbContext.Entry(entity).State;

        var addedEntity = dbContext.Set<T>().Add(entity).Entity;

        var stateAfterAdd = dbContext.Entry(entity).State;

        dbContext.SaveChanges();
        return addedEntity;
    }

    public IEnumerable<T> CreateRange(IEnumerable<T> entities)
    {
        dbContext.Set<T>().AddRange(entities);
        dbContext.SaveChanges();

        return entities;
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        dbContext.RemoveRange(entities);
    }

    public T Update(T entity) => dbContext.Set<T>().Update(entity).Entity;

    public void Delete(T entity) => dbContext.Set<T>().Remove(entity);

    public IQueryable<T> FindAll(bool trackChanges) =>
        !trackChanges ? dbContext.Set<T>().AsNoTracking() : dbContext.Set<T>();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        !trackChanges ? dbContext.Set<T>().Where(expression).AsNoTracking()
        : dbContext.Set<T>().Where(expression);

    public IQueryable<T> FindByConditionWithIncludes(
        Expression<Func<T, bool>> expression,
        bool trackChanges,
        params string[] includes)
    {
        var query = dbContext.Set<T>().Where(expression);

        if (!trackChanges)
            query = query.AsNoTracking();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }

    public IQueryable<T> FindAllWithIncludes(
        bool trackChanges,
        params string[] includes)
    {
        var query = dbContext.Set<T>().Where(a => 1 > 0);

        if (!trackChanges)
            query = query.AsNoTracking();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }


    public T Find(Expression<Func<T, bool>> expression) =>
        dbContext.Set<T>().FirstOrDefault(expression);
}

