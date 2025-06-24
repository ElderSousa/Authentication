using Microsoft.EntityFrameworkCore;
using MS_Authentication.Domain.Entities;
using MS_Authentication.Domain.Interfaces;
using MS_Authentication.Infrastructure.Data;

namespace MS_Authentication.Infrastructure.Repository;

public class RoleRepository : BaseRepository<Role>, IRoleRepositoy
{
    public RoleRepository(AuthDbContext authDbContext) : base(authDbContext){}

    public async Task<bool> CreateAsync(Role Role, CancellationToken cancellationToken)
    {
        return await GenericAddAsync(Role, cancellationToken);
    }

    public async Task<IEnumerable<Role>> GetByAllAsync(CancellationToken cancellationToken)
    {
        return await authDbContext.Roles
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Role?> GetByNomeAsync(string name, CancellationToken cancellationToken)
    {
        return await authDbContext.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Name == name && r.DeletedOn == null, cancellationToken);
    }

    public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await authDbContext.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id && r.DeletedOn == null, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Role role, CancellationToken cancellationToken)
    {
        return await GenericUpdateAsync(role, cancellationToken);
    }

    public async Task<bool> SoftDeleteAsync(Role role, CancellationToken cancellationToken)
    {
        return await GenericUpdateAsync(role, cancellationToken);
    }

    public async Task<bool> UserExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await EntityExistByIdAsync(u => u.Id == id && u.DeletedOn == null, cancellationToken);
    }
}
