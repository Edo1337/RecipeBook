using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Domain.Dto.Role;
using RecipeBook.Domain.Entity;
using RecipeBook.Domain.Enum;
using RecipeBook.Domain.Interfaces.Services;
using RecipeBook.Domain.Result;
using System.Net.Mime;

namespace RecipeBook.Api.Controllers
{
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    [Authorize(Roles = nameof(Roles.Admin))]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Создание роли
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// Request to create role:
        ///     
        ///     POST
        ///     {
        ///         "name": "string",
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Роль была создана</response>
        /// <response code="400">Роль не была создана</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<Role>>> Create([FromBody] CreateRoleDto dto)
        {
            var response = await _roleService.CreateRoleAsync(dto);
            if (response.isSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Обновление роли
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// Request to create recipe:
        ///     
        ///     PUT
        ///     {
        ///         "id": 1
        ///         "name": "Admin",
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Роль была обновлена</response>
        /// <response code="400">Роль не была обновлена</response>
        [HttpPut(template: "{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<Role>>> Update([FromBody] RoleDto dto)
        {
            var response = await _roleService.UpdateRoleAsync(dto);
            if (response.isSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Удаление роли
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Request to delete role:
        ///     
        ///     DELETE
        ///     {
        ///         "id": "1",
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Роль была удалена</response>
        /// <response code="400">Роль не была удалена</response>
        [HttpDelete(template:"{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<Role>>> Delete(long id)
        {
            var response = await _roleService.DeleteRoleAsync(id);
            if (response.isSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Добавление роли пользователю
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// Request to add role for user:
        ///     
        ///     POST
        ///     {
        ///         "login": "string",
        ///         "roleName": "string",
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Роль была назначена</response>
        /// <response code="400">Роль не была назначена</response>
        [HttpPost("addRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<Role>>> AddRoleForUser([FromBody] UserRoleDto dto)
        {
            var response = await _roleService.AddRoleForUserAsync(dto);
            if (response.isSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Удаление роли пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// Request to add role for user:
        ///     
        ///     POST
        ///     {
        ///         "login": "string",
        ///         "roleName": "string",
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Роль была назначена</response>
        /// <response code="400">Роль не была назначена</response>
        [HttpDelete("deleteRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<Role>>> DeleteRoleForUser([FromBody] UserRoleDto dto)
        {
            var response = await _roleService.DeleteRoleForUserAsync(dto);
            if (response.isSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
