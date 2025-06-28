using Library.Data.Entites.IdentityEntities;
using Library.Service.UserService.Dto;

namespace Library.Service.TokenService
{
    public interface ITokenService
    {
        string GenerateToken(AppUser appUser, string roles);
        RefreshToken GenerateRefreshToken();
        void SetRefreshTokenToCookie(string refreshToken, DateTime expires);
        RefreshToken RenewRefreshToken(AppUser user);
    }
}
