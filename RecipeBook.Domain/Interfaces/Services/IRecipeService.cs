using RecipeBook.Domain.Dto.Recipe;
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

        /// <summary>
        /// Создание рецепта с базовыми параметрами
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult> CreateRecipeAsync(CreateRecipeDto dto);
    }
}
