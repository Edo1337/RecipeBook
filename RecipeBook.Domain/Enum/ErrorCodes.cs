using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Domain.Enum
{
    public enum ErrorCodes
    {
        //Recipe 0-9
        RecipesNotFound = 0,
        RecipeNotFound = 1,
        RecipeAlreadyExists = 2,

        //User 11-20
        UserNotFound = 11,
        UserAlreadyExists = 12,
        UserUnauthorizedAccess = 13,

        // 
        PasswordNotEqualsPasswordConfirm = 21,
        PasswordIsWrong = 22,
        InvalidClientRequest = 23,

        //Role 31-40
        RoleAlreadyExists = 31,
        RoleNotFound = 32,

        InternalServerError = 10,
    }
}
