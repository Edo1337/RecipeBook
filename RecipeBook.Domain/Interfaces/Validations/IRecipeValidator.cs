using RecipeBook.Domain.Entity;
using RecipeBook.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Domain.Interfaces.Validations
{
    public interface IRecipeValidator: IBaseValidatior<Recipe>
    {
        /// <summary>
        /// Проверяем наличие рецепта, если такой уже есть, то создать такой же не выйдет
        /// Проверяем пользователя, если такой не найден, то такого пользователя нет
        /// </summary>
        /// <param name="recipe"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        BaseResult CreateValidator(Recipe recipe, User user);
    }
}
