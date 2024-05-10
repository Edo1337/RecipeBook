using RecipeBook.Domain.Entity;
using RecipeBook.Domain.Enum;
using RecipeBook.Domain.Interfaces.Validations;
using RecipeBook.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Application.Validations
{
    public class RecipeValidator : IRecipeValidator
    {
        public BaseResult CreateValidator(Recipe recipe, User user)
        {
            if (recipe != null)
            {
                return new BaseResult()
                {
                    ErrorMessage = "Рецепт с таким названием уже есть",
                    ErrorCode = (int)ErrorCodes.RecipeAlreadyExists
                };
            }

            if (user == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = "Пользователь не найден",
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }
            return new BaseResult();
        }

        public BaseResult ValidateOnNull(Recipe model)
        {
            if (model == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = "Рецепт не найден",
                    ErrorCode = (int)ErrorCodes.RecipeNotFound
                };
            }
            return new BaseResult();
        }
    }
}
