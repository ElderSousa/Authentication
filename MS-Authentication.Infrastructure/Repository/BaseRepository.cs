using Microsoft.EntityFrameworkCore;
using MS_Authentication.Infrastructure.Data;
using System.Linq.Expressions;

namespace MS_Authentication.Infrastructure.Repository;

public class BaseRepository<T> where T : class
{
    protected readonly AuthDbContext authDbContext;
    private readonly DbSet<T> _dbSet;
    public BaseRepository(AuthDbContext authDbContext)
    {
        this.authDbContext = authDbContext;
        _dbSet = authDbContext.Set<T>();
    }

    public async Task<bool> GenericAddAsync(T obj, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(obj, cancellationToken);
        var response = await authDbContext.SaveChangesAsync(cancellationToken);
        return response > 0;
    }

    public async Task<bool> GenericUpdateAsync(T obj, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(obj);
        var response = await authDbContext.SaveChangesAsync(cancellationToken);
        return response > 0;
    }

    public async Task<bool> GenericUpdateCompositekeyAsync(T obj, CancellationToken cancellationToken = default)
    {
        return await authDbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> EntityExistByIdAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }
}
