using AutoMapper;
using Domain.Users;
using Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MappingProfile
{
    public class UserMappinProfile : Profile
    {
        public UserMappinProfile()
        {
            CreateMap<User, UsersDTO>().ReverseMap();
            CreateMap<User, AddUserDTo>().ReverseMap();
            CreateMap<User, EditUserDTo>().ReverseMap();
            CreateMap<User, DeleteUserDTo>().ReverseMap();
            CreateMap<User, ChangePasswordUserDto>().ReverseMap();
        }
    }
}
