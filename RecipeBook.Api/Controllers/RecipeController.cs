using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Domain.Dto.Recipe;
using RecipeBook.Domain.Interfaces.Services;
using RecipeBook.Domain.Result;

namespace RecipeBook.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        /// <summary>
        /// Получение всех рецептов пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <remarks>
        /// Request to search for user recipes:
        ///     
        ///     GET
        ///     {
        ///         "userId": 1
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Рецепты получены</response>
        /// <response code="400">Рецепты не были получены</response>
        [HttpGet("recipes/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RecipeDto>>> GetUserRecipes(long userId)
        {
            var response = await _recipeService.GetRecipesAsync(userId);
            if (response.isSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Получение рецепта
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Request to search for a recipe by id:
        ///     
        ///     GET
        ///     {
        ///         "id": 1
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Рецепт получен</response>
        /// <response code="400">Рецепт не был получен</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RecipeDto>>> GetRecipe(long id)
        {
            var response = await _recipeService.GetRecipeByIdAsync(id);
            if (response.isSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Удаление рецепта
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Request to delete recipe by id:
        ///     
        ///     DELETE
        ///     {
        ///         "id": 1
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Рецепт был удален</response>
        /// <response code="400">Рецепт не был удален</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RecipeDto>>> Delete(long id)
        {
            var response = await _recipeService.DeleteRecipeAsync(id);
            if (response.isSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Создание рецепта
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// Request to create recipe:
        ///     
        ///     POST
        ///     {
        ///         "name": "string",
        ///         "description": "string",
        ///         "userId": 0
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Рецепт был создан</response>
        /// <response code="400">Рецепт не был создан</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RecipeDto>>> Create([FromBody] CreateRecipeDto dto)
        {
            var response = await _recipeService.CreateRecipeAsync(dto);
            if (response.isSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Обновление рецепта
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// Request to update recipe:
        ///     
        ///     PUT
        ///     {
        ///         "id": 0,
        ///         "name": "string",
        ///         "description": "string"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Рецепт был обновлен</response>
        /// <response code="400">Рецепт не был обновлен</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RecipeDto>>> Update([FromBody] UpdateRecipeDto dto)
        {
            var response = await _recipeService.UpdateRecipeAsync(dto);
            if (response.isSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

    }
}
