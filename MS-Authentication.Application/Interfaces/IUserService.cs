using MS_Authentication.Application.PaginationModel;
using MS_Authentication.Application.Responses;
using static MS_Authentication.Application.Requests.UserRequest;

namespace MS_Authentication.Application.Interfaces;

public interface IUserService
{
    Task<Response> CreateAsync(CreateUserRequest userRequest, CancellationToken cancellationToken);
    Task<UserResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<UserResponse> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<Pagination<RoleResponse>> GetByRolesAsync(Guid userId, int page, int pageSize, CancellationToken cancellationToken);
    Task<Pagination<UserResponse>> GetByAllAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<Response> UpdateAsync(UpdateUserRequest userRequest, CancellationToken cancellationToken);
    Task<Response> SoftDeleteAsync(Guid id, CancellationToken cancellationToken);
}
