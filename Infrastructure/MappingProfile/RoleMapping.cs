using Application.Permissions;
using AutoMapper;
using Domain.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MappingProfile
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<Role, RoleDto>()
               .ForMember(dest => dest.PermissionCount, option =>
                option.MapFrom(src => src.PermissionsRoles.Count));

            CreateMap<Role, CreateRoleDto>().ReverseMap();
            CreateMap<Role, EditRoleDto>().ReverseMap();
            CreateMap<Role, DeleteRoleDto>().ReverseMap();
        }
    }
}
