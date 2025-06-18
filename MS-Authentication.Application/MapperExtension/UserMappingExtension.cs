using MS_Authentication.Application.Responses;
using MS_Authentication.Domain.Entities;
using static MS_Authentication.Application.Requests.UserRequest;

namespace MS_Authentication.Application.MapperExtension;

public static class UserMappingExtension
{
    public static User MapToUser(this CreateUserRequest userRequest)
    {
        return new User 
        {
            Email = userRequest.Email,
            PasswordHash = userRequest.PasswordHash,
            Active = userRequest.Active,
            typeUserRole = userRequest.typeUserRole
        };
    }

    public static User MapToUser(this UpdateUserRequest userRequest)
    {
        return new User
        {
            Id = userRequest.Id,
            Email = userRequest.Email,
            PasswordHash = userRequest.PasswordHash,
            Active = userRequest.Active,
            typeUserRole = userRequest.typeUserRole
        };
    }

    public static UserResponse MapToUserResponse(this User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            Active = user.Active,
            typeUserRole = user.typeUserRole,
            CreatedOn = user.CreatedOn,
            CreatedBy = user.CreatedBy,
            ModifiedOn = user.ModifiedOn,
            ModifiedBy = user.ModifiedBy
        };
    }

    public static IEnumerable<UserResponse> MapToUserResponse(this IEnumerable<User> users)
    {
        return users.Select(u => u.MapToUserResponse());
    }
}
