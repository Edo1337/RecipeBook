using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.Dto.Role;
using RecipeBook.Domain.Dto.UserRole;
using RecipeBook.Domain.Entity;
using RecipeBook.Domain.Enum;
using RecipeBook.Domain.Interfaces.Databases;
using RecipeBook.Domain.Interfaces.Services;
using RecipeBook.Domain.Result;

namespace RecipeBook.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Role> _roleRepository;
        private readonly IBaseRepository<UserRole> _userRoleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IBaseRepository<User> userRepository, IBaseRepository<Role> roleRepository, IMapper mapper, IBaseRepository<UserRole> userRoleRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult<RoleDto>> CreateRoleAsync(CreateRoleDto dto)
        {
            var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
            if (role != null)
            {
                return new BaseResult<RoleDto>
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
            return new BaseResult<RoleDto>
            {
                Data = _mapper.Map<RoleDto>(role)
            };
        }

        public async Task<BaseResult<RoleDto>> DeleteRoleAsync(long id)
        {
            var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (role == null)
            {
                return new BaseResult<RoleDto>
                {
                    ErrorMessage = "Такой роли не существует",
                    ErrorCode = (int)ErrorCodes.RoleNotFound,
                };
            }
            _roleRepository.Remove(role);
            await _roleRepository.SaveChangesAsync();

            return new BaseResult<RoleDto>
            {
                Data = _mapper.Map<RoleDto>(role)
            };
        }

        public async Task<BaseResult<RoleDto>> UpdateRoleAsync(RoleDto dto)
        {
            var role = await _roleRepository.GetAll().FirstOrDefaultAsync(r => r.Id == dto.Id);
            if (role == null)
            {
                return new BaseResult<RoleDto>
                {
                    ErrorMessage = "Роль уже существует",
                    ErrorCode = (int)ErrorCodes.RoleNotFound,
                };
            }
            role.Name = dto.Name;
            var updatedRole = _roleRepository.Update(role);
            await _roleRepository.SaveChangesAsync();

            return new BaseResult<RoleDto>
            {
                Data = _mapper.Map<RoleDto>(updatedRole)
            };
        }

        public async Task<BaseResult<UserRoleDto>> AddRoleForUserAsync(UserRoleDto dto)
        {
            var user = await _userRepository.GetAll()
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Login == dto.Login);
            if (user == null)
            {
                return new BaseResult<UserRoleDto>
                {
                    ErrorMessage = "Пользователь не найден",
                    ErrorCode = (int)ErrorCodes.UserNotFound,
                };
            }

            var roles = user.Roles.Select(r => r.Name).ToArray();
            if (!roles.Any(x => x == dto.RoleName))
            {
                var role = await _roleRepository.GetAll().FirstOrDefaultAsync(r => r.Name == dto.RoleName);
                if (role == null)
                {
                    return new BaseResult<UserRoleDto>
                    {
                        ErrorMessage = "Роль не найдена",
                        ErrorCode = (int)ErrorCodes.RoleNotFound,
                    };
                }

                UserRole userRole = new UserRole()
                {
                    UserId = user.Id,
                    RoleId = role.Id
                };
                await _userRoleRepository.CreateAsync(userRole);

                return new BaseResult<UserRoleDto>()
                {
                    Data = new UserRoleDto()
                    {
                        Login = user.Login,
                        RoleName = role.Name
                    }
                };
            }

            return new BaseResult<UserRoleDto>
            {
                ErrorMessage = "Пользователь уже имеет эту роль",
                ErrorCode = (int)ErrorCodes.UserAlreadyExistsThisRole,
            };
        }

        public async Task<BaseResult<UserRoleDto>> DeleteRoleForUserAsync(DeleteUserRoleDto dto)
        {
            var user = await _userRepository.GetAll()
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Login == dto.Login);
            if (user == null)
            {
                return new BaseResult<UserRoleDto>
                {
                    ErrorMessage = "Пользователь не найден",
                    ErrorCode = (int)ErrorCodes.UserNotFound,
                };
            }

            var role = user.Roles.FirstOrDefault(r => r.Id == dto.RoleId);
            if (role == null)
            {
                return new BaseResult<UserRoleDto>
                {
                    ErrorMessage = "Роль не найдена",
                    ErrorCode = (int)ErrorCodes.RoleNotFound,
                };
            }

            var userRole = await _userRoleRepository.GetAll()
                .Where(ur => ur.RoleId == role.Id)
                .FirstOrDefaultAsync(ur => ur.UserId == user.Id);
            _userRoleRepository.Remove(userRole);
            await _userRoleRepository.SaveChangesAsync();

            return new BaseResult<UserRoleDto>()
            {
                Data = new UserRoleDto()
                {
                    Login = user.Login,
                    RoleName = role.Name
                }
            };
        }

        public async Task<BaseResult<UserRoleDto>> UpdateRoleForUserAsync(UpdateUserRoleDto dto)
        {
            var user = await _userRepository.GetAll()
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Login == dto.Login);
            if (user == null)
            {
                return new BaseResult<UserRoleDto>
                {
                    ErrorMessage = "Пользователь не найден",
                    ErrorCode = (int)ErrorCodes.UserNotFound,
                };
            }

            var role = user.Roles.FirstOrDefault(r => r.Id == dto.FromRoleId);
            if (role == null)
            {
                return new BaseResult<UserRoleDto>
                {
                    ErrorMessage = "Роль не найдена",
                    ErrorCode = (int)ErrorCodes.RoleNotFound,
                };
            }

            var newRoleForUser = await _roleRepository.GetAll().FirstOrDefaultAsync(r => r.Id == dto.ToRoleId);
            if (newRoleForUser == null)
            {
                return new BaseResult<UserRoleDto>
                {
                    ErrorMessage = "Роль не найдена",
                    ErrorCode = (int)ErrorCodes.RoleNotFound,
                };
            }

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var userRole = await _unitOfWork.UserRoles.GetAll()
                        .Where(ur => ur.RoleId == role.Id)
                        .FirstAsync(ur => ur.UserId == user.Id);

                    _unitOfWork.UserRoles.Remove(userRole);
                    await _unitOfWork.SaveChangesAsync();

                    var newUserRole = new UserRole()
                    {
                        UserId = user.Id,
                        RoleId = newRoleForUser.Id,
                    };
                    await _unitOfWork.UserRoles.CreateAsync(newUserRole);
                    await _unitOfWork.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                }
            }
            return new BaseResult<UserRoleDto>()
            {
                Data = new UserRoleDto()
                {
                    Login = user.Login,
                    RoleName = newRoleForUser.Name
                }
            };
        }
    }
}
