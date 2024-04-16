using AutoMapper;
using RecipeBook.Domain.Dto.Recipe;

namespace RecipeBook.Application.Mapping
{
    public class RecipeMapping : Profile
    {
        public RecipeMapping()
        {
            CreateMap<Recipe, RecipeDto>()
                .ReverseMap();
        }
    }
}
