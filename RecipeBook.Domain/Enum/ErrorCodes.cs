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
        //User 10-20

        RecipesNotFound = 0,
        RecipeNotFound = 1,
        InternalServerError = 10,
    }
}
