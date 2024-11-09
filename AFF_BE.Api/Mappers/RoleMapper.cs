using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Models.Identity.Permission;
using AFF_BE.Core.Models.Identity.Role;
using AutoMapper;

namespace AFF_BE.Api.Mappers
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {

            CreateMap<Role, CreateRoleRequest>();
            CreateMap<CreateRoleRequest, Role>();
            CreateMap<EditRoleRequest, Role>();
            //CreateMap<Role, RoleDto>();
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.RolePermissions.Select(rp => rp.Permission)));
            CreateMap<RoleDto, Role>();
        }
    }
}
