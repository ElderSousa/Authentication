using MS_Authentication.Domain.Entities;
using System.Security.Claims;

namespace MS_Authentication.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user, IEnumerable<Role> roles);
    string GenerateRefreshToken();
    ClaimsPrincipal GetByPrincipalFromExpiredToken(string token);
}
