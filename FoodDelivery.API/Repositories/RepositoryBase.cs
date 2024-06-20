using System.Linq.Expressions;
using FoodDelivery.API.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Repositories;

public interface IRepositoryBase<T>
{
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition);
    IQueryable<T> FindAll();
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected ApplicationDbContext Context { get; set; }
    protected IUnitOfWork UnitOfWork { get; set; }

    public RepositoryBase(ApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        Context = context;
        UnitOfWork = unitOfWork;
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition)
    {
        return Context.Set<T>().Where(condition).AsNoTracking();
    }

    public IQueryable<T> FindAll()
    {
        return Context.Set<T>().AsNoTracking();
    }

    public async Task CreateAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        Context.Set<T>().Update(entity);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        Context.Set<T>().Remove(entity);
        await UnitOfWork.SaveChangesAsync();
    }
}