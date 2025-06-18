using MS_Authentication.Domain.Entities;

namespace MS_Authentication.Domain.Interfaces;

public interface IUserRepository
{
    Task<bool> CreateAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<IEnumerable<Role>> GetRolesAsync(Guid userId, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(User user, CancellationToken cancellationToken);
    Task<bool> SoftDeleteAsync(User user, CancellationToken cancellationToken);
    Task<bool> UserExistAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> EmailExistAsync(string email, CancellationToken cancellationToken);
}
