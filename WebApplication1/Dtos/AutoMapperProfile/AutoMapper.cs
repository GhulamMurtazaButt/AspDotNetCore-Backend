using AutoMapper;
using WebApplication1.Dtos.User;
using DataLibrary.Models;

namespace WebApplication1.Dtos.AutoMapperProfile
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {

            CreateMap<Users, GetUserDto>();
            CreateMap<UpdateUserDto, Users>();

        }
    }
}
