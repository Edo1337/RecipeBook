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
            CreateMap<Recipe, RecipeDto>()
                .ForCtorParam(ctorParamName: "Id", m => m.MapFrom(s => s.Id))
                .ForCtorParam(ctorParamName: "Name", m => m.MapFrom(s => s.Name))
                .ForCtorParam(ctorParamName: "Description", m => m.MapFrom(s => s.Description))
                .ForCtorParam(ctorParamName: "CreatedAt", m => m.MapFrom(s => s.CreatedAt))
                .ReverseMap();
        }
    }
}
