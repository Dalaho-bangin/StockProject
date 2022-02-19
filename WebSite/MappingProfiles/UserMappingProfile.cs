using AutoMapper;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.MappingProfiles
{
    public class UserMappinProfile : Profile
    {
        public UserMappinProfile()
        {
            CreateMap<User, Application.Users.UsersDTO>().ReverseMap();
        }
    }
}
