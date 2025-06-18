using MS_Authentication.Application.Responses;
using MS_Authentication.Domain.Entities;
using System.Data;
using static MS_Authentication.Application.Requests.UserRoleRequest;

namespace MS_Authentication.Application.MapperExtension;

public static class UserRoleMappingExtension
{
    public static UserRole MapToUseRole(this CreateUserRoleRequest createUserRoleRequest)
    {
        return new UserRole
        {
            UserId = createUserRoleRequest.UserId,
            RoleId = createUserRoleRequest.RoleId,
        };
    }

    public static UserRole MapToUseRole(this UpdateUserRoleRequest updateUserRoleRequest)
    {
        return new UserRole
        {
            UserId = updateUserRoleRequest.UserId,
            RoleId = updateUserRoleRequest.RoleId,
        };
    }

    public static UserRoleResponse MapToUseRoleResponse(this UserRole userRole)
    {
        return new UserRoleResponse
        {
            UserId = userRole.UserId,
            RoleId = userRole.RoleId,
            CreatedOn = userRole.CreatedOn,
            CreatedBy = userRole.CreatedBy,
            ModifiedOn = userRole.ModifiedOn,
            ModifiedBy = userRole.ModifiedBy
        };
    }

    public static IEnumerable<UserRoleResponse> MapToUseRoleResponse(this IEnumerable<UserRole> userRoles)
    {
        return userRoles.Select(ur => ur.MapToUseRoleResponse());
    }
}
