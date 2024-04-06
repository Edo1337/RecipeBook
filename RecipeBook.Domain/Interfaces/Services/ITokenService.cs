using RecipeBook.Domain.Dto;
using RecipeBook.Domain.Result;
using System.Security.Claims;

namespace RecipeBook.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto);
    }
}
