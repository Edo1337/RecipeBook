using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Application.Resourses;
using RecipeBook.Domain.Dto;
using RecipeBook.Domain.Dto.Recipe;
using RecipeBook.Domain.Dto.User;
using RecipeBook.Domain.Enum;
using RecipeBook.Domain.Interfaces.Services;
using RecipeBook.Domain.Result;
using Serilog;
using System.Security.Cryptography;
using System.Text;

namespace RecipeBook.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AuthService(IBaseRepository<User> userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<BaseResult<TokenDto>> Login(LoginUserDto dto)
        {
            return null;
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
            return BitConverter.ToString(bytes).ToLower();
        }
    }
}