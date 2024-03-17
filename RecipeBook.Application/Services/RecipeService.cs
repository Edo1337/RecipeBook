using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.DTO;
using RecipeBook.Domain.Interfaces.Services;
using RecipeBook.Domain.Result;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IBaseRepository<Recipe> _recipeRepository;
        private readonly ILogger _logger;

        public RecipeService(IBaseRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<CollectionResult<RecipeDto>> GetRecipesAsync(long userId)
        {
            RecipeDto[] recipes;
            try
            {
                recipes = await _recipeRepository.GetAll()
                    .Where(x => x.UserId == userId)
                    .Select(x => new RecipeDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
                    .ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<RecipeDto>()
                {
                    ErrorMessage = "Внутренняя ошибка сервера",
                    ErrorCode = 10
                };
            }


            return null;
        }
    }
}
