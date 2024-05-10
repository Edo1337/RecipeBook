using AutoMapper;
using RecipeBook.Domain.Dto.User;
using RecipeBook.Domain.Entity;

namespace RecipeBook.Application.Mapping
{
    public class UserMapping: Profile
    {
        public UserMapping() 
        { 
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
