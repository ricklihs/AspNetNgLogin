using AutoMapper;
using WebApi.Dtos;
using WebApi.Entities;

namespace WebApi.Helpers
{
    //Profile :Provides a named configuration for maps. 
    //Naming conventions become scoped per profile.
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile(){
            CreateMap<User,UserDto>();
            CreateMap<UserDto,User>();
            CreateMap<Comment,CommentDto>();
            CreateMap<CommentDto,Comment>();
        }
    }
}