using Microsoft.AspNetCore.Mvc;
using RecipeBook.Application.Services;
using RecipeBook.Domain.Dto;
using RecipeBook.Domain.Dto.User;
using RecipeBook.Domain.Interfaces.Services;
using RecipeBook.Domain.Result;

namespace RecipeBook.Api.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<BaseResult<TokenDto>>> Register([FromBody] RegisterUserDto dto)
        {
            var response = await _authService.Register(dto);
            if (response.isSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        
        /// <summary>
        /// Логин пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<BaseResult<TokenDto>>> Login([FromBody] LoginUserDto dto)
        {

        }
    }
}
