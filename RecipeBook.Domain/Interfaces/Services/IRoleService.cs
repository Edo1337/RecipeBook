using RecipeBook.Domain.Dto.Role;
using RecipeBook.Domain.Entity;
using RecipeBook.Domain.Result;

namespace RecipeBook.Domain.Interfaces.Services
{
    /// <summary>
    /// Сервис для управления ролями
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Создание роли
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<RoleDto>> CreateRoleAsync(CreateRoleDto dto);

        /// <summary>
        /// Удаление роли
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<RoleDto>> DeleteRoleAsync(long id);

        /// <summary>
        /// Изменение роли
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<RoleDto>> UpdateRoleAsync(RoleDto dto);

        /// <summary>
        /// Добавление роли для пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<UserRoleDto>> AddRoleForUserAsync(UserRoleDto dto);

        /// <summary>
        /// Удаление роли у пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<UserRoleDto>> DeleteRoleForUserAsync(UserRoleDto dto);

        /// <summary>
        /// Обновление роли для пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<UserRoleDto>> UpdateRoleForUserAsync(UserRoleDto dto);
    }
}
