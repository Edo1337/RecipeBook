using AutoMapper;
using RecipeBook.Domain.Dto.Recipe;
using RecipeBook.Domain.Entity;

namespace RecipeBook.Application.Mapping
{
    public class RecipeMapping : Profile
    {
        public RecipeMapping()
        {
            CreateMap<Recipe, RecipeDto>().ReverseMap();
        }
    }
}
