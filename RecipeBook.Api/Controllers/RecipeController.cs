using Microsoft.AspNetCore.Mvc;
using RecipeBook.Domain.Dto.Recipe;
using RecipeBook.Domain.Interfaces.Services;
using RecipeBook.Domain.Result;

namespace RecipeBook.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RecipeDto>>> GetRecipe(long id)
        {
            var response = await _recipeService.GetRecipeByIdAsync(id);
            if(response.isSuccess) 
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

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
