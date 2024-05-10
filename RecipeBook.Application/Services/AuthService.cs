using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Application.Resourses;
using RecipeBook.Domain.Dto;
using RecipeBook.Domain.Dto.Recipe;
using RecipeBook.Domain.Dto.User;
using RecipeBook.Domain.Entity;
using RecipeBook.Domain.Enum;
using RecipeBook.Domain.Interfaces.Services;
using RecipeBook.Domain.Result;
using Serilog;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RecipeBook.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<UserToken> _userTokenRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AuthService(IBaseRepository<User> userRepository, ILogger logger, ITokenService tokenService,
            IBaseRepository<UserToken> userTokenRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _tokenService = tokenService;
            _userTokenRepository = userTokenRepository;
            _mapper = mapper;
        }

        public async Task<BaseResult<TokenDto>> Login(LoginUserDto dto)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == dto.Login);
                if (user == null)
                {
                    return new BaseResult<TokenDto>()
                    {
                        ErrorMessage = "Пользователь не найден",
                        ErrorCode = (int)ErrorCodes.UserNotFound
                    };
                }
                if (!IsVerifyPassword(user.Password, dto.Password))
                {
                    return new BaseResult<TokenDto>()
                    {
                        ErrorMessage = "Неверный пароль",
                        ErrorCode = (int)ErrorCodes.PasswordIsWrong
                    };
                }

                var userToken = await _userTokenRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == user.Id);

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Role, "User")
                };
                var accessToken = _tokenService.GenerateAccessToken(claims);

                var refreshToken = _tokenService.GenerateRefreshToken();
                var refreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                if (userToken == null)
                {
                    userToken = new UserToken()
                    {
                        UserId = user.Id,
                        RefreshToken = refreshToken,
                        RefreshTokenExpiryTime = refreshTokenExpiryTime
                    };
                    await _userTokenRepository.CreateAsync(userToken);
                }
                else
                {
                    userToken.RefreshToken = refreshToken;
                    userToken.RefreshTokenExpiryTime = refreshTokenExpiryTime;

                    await _userTokenRepository.UpdateAsync(userToken);

                }

                return new BaseResult<TokenDto>()
                {
                    Data = new TokenDto()
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = "Внутренняя ошибка сервера",
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        public async Task<BaseResult<UserDto>> Register(RegisterUserDto dto)
        {
            if (dto.Password != dto.PasswordConfirm)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = "Пароль и пароль подтверждения не равны",
                    ErrorCode = (int)ErrorCodes.PasswordNotEqualsPasswordConfirm
                };
            }

            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == dto.Login);
                if (user != null)
                {
                    return new BaseResult<UserDto>()
                    {
                        ErrorMessage = "Пользователь с такими данными уже существует",
                        ErrorCode = (int)ErrorCodes.UserAlreadyExists
                    };
                }
                var hashUserPassword = HashPassword(dto.Password);

                user = new User()
                {
                    Login = dto.Login,
                    Password = hashUserPassword
                };
                await _userRepository.CreateAsync(user);
                return new BaseResult<UserDto>()
                {
                    Data = _mapper.Map<UserDto>(user)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = "Внутренняя ошибка сервера",
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        private string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool IsVerifyPassword(string userPasswordHash, string userPassword)
        {
            var hash = HashPassword(userPassword);
            return userPasswordHash == hash;
        }
    }
}