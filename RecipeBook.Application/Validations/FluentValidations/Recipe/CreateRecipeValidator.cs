using FluentValidation;
using RecipeBook.Domain.Dto.Recipe;

namespace RecipeBook.Application.Validations.FluentValidations.Recipe
{
    public class CreateRecipeValidator : AbstractValidator<CreateRecipeDto>
    {
        public CreateRecipeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);//правило = поле не пустое и макс длина 200
            RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        }
    }
}
