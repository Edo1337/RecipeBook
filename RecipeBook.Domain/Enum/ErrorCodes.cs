using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Domain.Enum
{
    public enum ErrorCodes
    {
        //Recipe 0-10
        RecipesNotFound = 0,
        RecipeNotFound = 1,
        RecipeAlreadyExists = 2,

        //User 10-20
        UserNotFound = 11,

        InternalServerError = 10,
    }
}
