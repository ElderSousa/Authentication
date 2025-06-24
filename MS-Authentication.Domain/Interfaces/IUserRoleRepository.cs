using MS_Authentication.Domain.Entities;

namespace MS_Authentication.Domain.Interfaces;

public interface IUserRoleRepository
{
    Task<bool> CreateAsync(UserRole UserRole, CancellationToken cancellationToken);
    Task<UserRole> GetByIdAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
    Task<IEnumerable<UserRole>> GetByAllAsync(CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UserRole userRole, CancellationToken cancellationToken);
    Task<bool> SoftDeleteAsync(UserRole userRole, CancellationToken cancellationToken);
    Task<bool> UserExistAsync(Guid id, CancellationToken cancellationToken);
}
