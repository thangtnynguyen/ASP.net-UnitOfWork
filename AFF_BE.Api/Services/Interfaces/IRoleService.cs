using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Identity.Role;

namespace AFF_BE.Api.Services.Interfaces
{
    public interface IRoleService
    {
        Task<PagingResult<RoleDto>> GetPaging(GetRoleRequest request);

        Task<RoleDto> GetById(EntityIdentityRequest<int> request);

        Task<PagingResult<RoleDto>> GetByUser(GetRoleByUserRequest request);

        Task<RoleDto> Create(CreateRoleRequest request);

        Task<RoleDto> Edit(EditRoleRequest request);

        Task<RoleDto> Delete(int id);

        Task<List<RoleDto>> DeleteMultiple(List<int?> ids);

        Task<List<User>> GetUsersInRoleByIdAsync(int roleId);


    }
}
