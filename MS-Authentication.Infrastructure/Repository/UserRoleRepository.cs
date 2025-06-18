using Microsoft.EntityFrameworkCore;
using MS_Authentication.Domain.Entities;
using MS_Authentication.Domain.Interfaces;
using MS_Authentication.Infrastructure.Data;

namespace MS_Authentication.Infrastructure.Repository;

public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(AuthDbContext authDbContext) : base(authDbContext) { }

    public async Task<bool> CreateAsync(UserRole userRole, CancellationToken cancellationToken)
    {
        return await GenericAddAsync(userRole, cancellationToken);
    }

    public async Task<IEnumerable<UserRole>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await authDbContext.UserRoles
            .AsNoTracking()
            .Where(ur => ur.DeletedOn == null)
            .ToListAsync(cancellationToken);
    }

    public async Task<UserRole> GetIdAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        return await authDbContext.UserRoles
            .AsNoTracking()
            .FirstAsync(ur => ur.Id == userId && ur.RoleId == roleId && ur.DeletedOn == null, cancellationToken);
    }

    public async Task<bool> UpdateAsync(UserRole userRole, CancellationToken cancellationToken)
    {
        return await GenericUpdateAsync(userRole, cancellationToken);
    }

    public async Task<bool> SoftDeleteAsync(UserRole userRole, CancellationToken cancellationToken)
    {
        return await GenericUpdateAsync(userRole, cancellationToken);
    }

    public async Task<bool> UserExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await EntityExistByIdAsync(u => u.Id == id && u.DeletedOn == null, cancellationToken);
    }
}
