using RecipeBook.Domain.Result;

namespace RecipeBook.Domain.Interfaces.Validations
{
    public interface IBaseValidatior<in T> where T : class
    {
        BaseResult ValidateOnNull(T model);
    }
}
