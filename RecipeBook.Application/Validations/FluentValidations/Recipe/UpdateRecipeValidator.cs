using FluentValidation;
using RecipeBook.Domain.Dto.Recipe;

namespace RecipeBook.Application.Validations.FluentValidations.Recipe
{
    public class UpdateRecipeValidator : AbstractValidator<UpdateRecipeDto>
    {
        public UpdateRecipeValidator()
        {
            RuleFor(x => x.Id).NotEmpty();//правило = поле не пустое
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        }
    }
}
