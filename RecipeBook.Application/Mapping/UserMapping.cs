using AutoMapper;
using RecipeBook.Domain.Dto.User;

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
