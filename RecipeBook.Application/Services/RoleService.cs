using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.Dto.Role;
using RecipeBook.Domain.Entity;
using RecipeBook.Domain.Enum;
using RecipeBook.Domain.Interfaces.Services;
using RecipeBook.Domain.Result;

namespace RecipeBook.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Role> _roleRepository;

        public RoleService(IBaseRepository<User> userRepository, IBaseRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<BaseResult<Role>> CreateRoleAsync(RoleDto dto)
        {
            var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
            if (role != null)
            {
                return new BaseResult<Role>
                {
                    ErrorMessage = "Роль уже существует",
                    ErrorCode = (int)ErrorCodes.RoleAlreadyExists,
                };
            }
            role = new Role()
            {
                Name = dto.Name,
            };
            await _roleRepository.CreateAsync(role);
            return new BaseResult<Role>
            {
                Data = role,
            };
        }

        public async Task<BaseResult<Role>> DeleteRoleAsync(long id)
        {
            var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (role == null)
            {
                return new BaseResult<Role>
                {
                    ErrorMessage = "Роль уже существует",
                    ErrorCode = (int)ErrorCodes.RoleNotFound,
                };
            }
            await _roleRepository.RemoveAsync(role);
            return new BaseResult<Role>
            {
                Data = role,
            };
        }

        public async Task<BaseResult<Role>> UpdateRoleAsync(UpdateRoleDto dto)
        {
            var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (role == null)
            {
                return new BaseResult<Role>
                {
                    ErrorMessage = "Роль уже существует",
                    ErrorCode = (int)ErrorCodes.RoleNotFound,
                };
            }
            role.Name = dto.Name;
            await _roleRepository.UpdateAsync(role);
            return new BaseResult<Role>
            {
                Data = role,
            };
        }
    }
}
