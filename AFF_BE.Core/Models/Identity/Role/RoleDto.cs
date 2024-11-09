

using AFF_BE.Core.Models.Identity.Permission;

namespace AFF_BE.Core.Models.Identity.Role
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? NormalizedName { get; set; }

        public List<PermissionDto> Permissions { get; set; }

    }
}
