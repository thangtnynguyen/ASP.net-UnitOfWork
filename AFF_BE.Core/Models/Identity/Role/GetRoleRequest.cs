
using AFF_BE.Core.Models.Common;

namespace AFF_BE.Core.Models.Identity.Role
{
    public class GetRoleRequest: PagingRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
