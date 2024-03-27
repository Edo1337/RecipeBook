using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Application.Mapping;
using RecipeBook.Application.Services;
using RecipeBook.Application.Validations.FluentValidations.Recipe;
using RecipeBook.Domain.Dto.Recipe;
using RecipeBook.Domain.Interfaces.Services;

namespace RecipeBook.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(RecipeMapping));

            InitServices(services);
        }

        private static void InitServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateRecipeDto>, CreateRecipeValidator>();
            services.AddScoped<IValidator<UpdateRecipeDto>, UpdateRecipeValidator>();
    
            services.AddScoped<IRecipeService, RecipeService>();
        }
    }
}
