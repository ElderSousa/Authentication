using MS_Authentication.Domain.Entities;

namespace MS_Authentication.Domain.Interfaces;

public interface IRoleRepositoy
{
    Task<bool> CreateAsync(Role Role, CancellationToken cancellationToken);
    Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Role?> GetByNomeAsync(string nome, CancellationToken cancellationToken);
    Task<IEnumerable<Role>> GetByAllAsync(CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Role role, CancellationToken cancellationToken);
    Task<bool> SoftDeleteAsync(Role role, CancellationToken cancellationToken);
    Task<bool> UserExistAsync(Guid id, CancellationToken cancellationToken);
}
