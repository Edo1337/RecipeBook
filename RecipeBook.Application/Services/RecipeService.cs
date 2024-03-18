using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.DTO;
using RecipeBook.Domain.Enum;
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

        /// <inheritdoc />
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
                    ErrorMessage = "Внутренняя ошибка сервера", //Можно не хардкодиоть и сделать через ../Resourses/ErrorMessage.resx, но пока так
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }

            if (recipes.Length == 0)
            {
                _logger.Warning("Рецепты не найдены", recipes.Length);
                return new CollectionResult<RecipeDto>()
                {
                    ErrorMessage = "Рецепты не найдены",
                    ErrorCode = (int)ErrorCodes.RecipesNotFound
                };
            }

            return new CollectionResult<RecipeDto>()
            {
                Data = recipes,
                Count = recipes.Length
            };
        }

        /// <inheritdoc />
        public async Task<BaseResult<RecipeDto>> GetRecipeByIdAsync(long id)
        {
            RecipeDto? recipe;
            try
            {
                recipe = await _recipeRepository.GetAll()
                    .Select(x => new RecipeDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<RecipeDto>()
                {
                    ErrorMessage = "Внутренняя ошибка сервера",
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
            if (recipe == null)
            {
                _logger.Warning($"Рецепт c {id} не найден", id);
                return new BaseResult<RecipeDto>()
                {
                    ErrorMessage = "Рецепт не найден",
                    ErrorCode = (int)ErrorCodes.RecipeNotFound
                };
            }


            return new BaseResult<RecipeDto>()
            {
                Data = recipe
            };
        }
    }
}
