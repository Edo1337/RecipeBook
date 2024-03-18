using RecipeBook.Domain.DTO;
using RecipeBook.Domain.Result;

namespace RecipeBook.Domain.Interfaces.Services
{
    /// <summary>
    /// Сервис отвечает за работу с доменной части рецепта (Recipe)
    /// </summary>
    public interface IRecipeService
    {
        /// <summary>
        /// Получение всех рецептов пользователя
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<CollectionResult<RecipeDto>> GetRecipesAsync(long UserId);

        /// <summary>
        /// Получение рецепта по Id
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<BaseResult<RecipeDto>> GetRecipeByIdAsync(long UserId);


    }
}
