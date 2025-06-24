using MS_Authentication.Application.PaginationModel;
using MS_Authentication.Application.Responses;
using static MS_Authentication.Application.Requests.RoleRequest;

namespace MS_Authentication.Application.Interfaces;

public interface IRoleService
{
    Task<Response> CreateAsync(CreateRoleRequest roleRequest, CancellationToken cancellationToken);
    Task<RoleResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Pagination<RoleResponse>> GetByAllAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<Response> UpdateAsync(UpdateRoleRequest roleRequest, CancellationToken cancellationToken);
    Task<Response> SoftDeleteAsync(Guid id, CancellationToken cancellationToken);
}
