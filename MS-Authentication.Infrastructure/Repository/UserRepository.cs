using Microsoft.EntityFrameworkCore;
using MS_Authentication.Domain.Entities;
using MS_Authentication.Domain.Interfaces;
using MS_Authentication.Infrastructure.Data;

namespace MS_Authentication.Infrastructure.Repository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AuthDbContext authDbContext): base(authDbContext) { }

    public async Task<bool> CreateAsync(User user, CancellationToken cancellationToken)
    {
        return await GenericAddAsync(user, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetByAllAsync(CancellationToken cancellationToken)
    {
        return await authDbContext.Users
            .AsNoTracking()
            .Where(u => u.DeletedOn == null)
            .ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await authDbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id && u.DeletedOn == null, cancellationToken);
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return authDbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email && u.DeletedOn == null);
    }

    public async Task<IEnumerable<Role>> GetByRolesAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await authDbContext.UserRoles
            .AsNoTracking()
           
            .Include(ur => ur.Role)
            .Where(ur => ur.UserId == userId && ur.DeletedOn == null && ur.Role.DeletedOn == null && ur.User.DeletedOn == null)
            .Select(ur => ur.Role)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        return await GenericUpdateAsync(user, cancellationToken);
    }

    public async Task<bool> SoftDeleteAsync(User user, CancellationToken cancellationToken)
    {
        return await GenericUpdateAsync(user, cancellationToken);
    }

    public async Task<bool> UserExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await EntityExistByIdAsync(u => u.Id == id && u.DeletedOn == null, cancellationToken);
    }

    public async Task<bool> EmailExistAsync(string email, CancellationToken cancellationToken)
    {
        return await EntityExistByIdAsync(u => u.Email == email && u.DeletedOn == null, cancellationToken);
    }

}