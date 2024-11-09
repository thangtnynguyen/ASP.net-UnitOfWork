using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Models.Identity.Permission;
using AutoMapper;

namespace AFF_BE.Api.Mappers
{
    public class PermissionMapper : Profile
    {
        public PermissionMapper()
        {

            CreateMap<Permission, PermissionDto>();
            CreateMap<PermissionDto, Permission>();
            CreateMap<CreatePermissionRequest, Permission>();

        }
    }
}
