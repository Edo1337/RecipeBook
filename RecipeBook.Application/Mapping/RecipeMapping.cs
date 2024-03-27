using AutoMapper;
using RecipeBook.Domain.Dto.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Application.Mapping
{
    public class RecipeMapping: Profile
    {
        public RecipeMapping()
        {
            CreateMap<Recipe, RecipeDto>().ReverseMap();
        }
    }
}
