using AutoMapper;
using RecipeBook.Domain.Dto.Role;
using RecipeBook.Domain.Entity;

namespace RecipeBook.Application.Mapping
{
    public class RoleMapping : Profile
    {
        public RoleMapping() 
        { 
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
