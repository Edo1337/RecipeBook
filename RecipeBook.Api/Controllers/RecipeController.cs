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
    }
}
