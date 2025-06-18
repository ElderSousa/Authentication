using MS_Authentication.Application.Responses;
using MS_Authentication.Domain.Entities;

namespace MS_Authentication.Application.MapperExtension
{
    public static class RefreshTokenMapping
    {
        public static RefreshTokenResponse MapToRefreshTokeResponse(this RefreshToken refreshToken)
        {
            return new RefreshTokenResponse
            {
                Id = refreshToken.Id,
                UserId = refreshToken.UserId,
                Token =refreshToken.Token,
                Expiration = refreshToken.ExpirationDate,
                IsRevoked = refreshToken.IsRevoked,
                CreatedBy = refreshToken.CreatedBy,
                CreatedOn = refreshToken.CreatedOn,
                ModifiedBy = refreshToken.ModifiedBy,
                ModifiedOn = refreshToken.ModifiedOn
            };
        }
    }
}
