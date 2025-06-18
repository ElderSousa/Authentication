using MS_Authentication.Application.Responses;
using MS_Authentication.Domain.Entities;
using static MS_Authentication.Application.Requests.RoleRequest;

namespace MS_Authentication.Application.MapperExtension;

public static class RoleMappingExtension
{
    public static Role MapToRole(this CreateRoleRequest createRoleRequest)
    {
        return new Role
        {
            Name = createRoleRequest.Name,
            Description = createRoleRequest.Description,
        };
    }

    public static Role MapToRole(this UpdateRoleRequest updateCreateRoleRequest)
    {
        return new Role
        {
            Id = updateCreateRoleRequest.Id,
            Name = updateCreateRoleRequest.Name,
            Description = updateCreateRoleRequest.Description,
        };
    }

    public static RoleResponse MapToRoleResponse(this Role role)
    {
        return new RoleResponse
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            CreatedOn = role.CreatedOn,
            CreatedBy = role.CreatedBy,
            ModifiedOn = role.ModifiedOn,
            ModifiedBy = role.ModifiedBy
        };
    }

    public static IEnumerable<RoleResponse> MapToRoleResponse(this IEnumerable<Role> roles)
    {
        return roles.Select(r => r.MapToRoleResponse());
    }
}
