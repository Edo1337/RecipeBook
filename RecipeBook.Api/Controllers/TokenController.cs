using Microsoft.AspNetCore.Mvc;
using RecipeBook.Application.Services;
using RecipeBook.Domain.Dto;
using RecipeBook.Domain.Interfaces.Services;
using RecipeBook.Domain.Result;

namespace RecipeBook.Api.Controllers
{
    [ApiController]
    [Route("refresh")]
    public class TokenController : Controller
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResult<TokenDto>>> RefreshToken([FromBody] TokenDto dto)
        {
            var response = await _tokenService.RefreshToken(dto);
            if (response.isSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
