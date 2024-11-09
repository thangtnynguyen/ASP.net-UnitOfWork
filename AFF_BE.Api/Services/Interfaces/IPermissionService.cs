using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Identity.Permission;

namespace AFF_BE.Api.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<PagingResult<PermissionDto>> GetPaging(GetPermissionRequest request);

        Task<List<PermissionDto>> GetByRoleId(int roleId);

        Task<PermissionDto> Create(CreatePermissionRequest request);

    }
}
