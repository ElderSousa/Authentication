using MS_Authentication.Application.PaginationModel;
using MS_Authentication.Application.Responses;
using static MS_Authentication.Application.Requests.UserRoleRequest;

namespace MS_Authentication.Application.Interfaces;

public interface IUserRoleService
{
    Task<Response> CreateAsync(CreateUserRoleRequest userRoleRequest, CancellationToken cancellationToken);
    Task<UserRoleResponse> GetByIdAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
    Task<Pagination<UserRoleResponse>> GetByAllAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<Response> UpdateAsync(UpdateUserRoleRequest userRoleRequest, CancellationToken cancellationToken);
    Task<Response> SoftDeleteAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
}
