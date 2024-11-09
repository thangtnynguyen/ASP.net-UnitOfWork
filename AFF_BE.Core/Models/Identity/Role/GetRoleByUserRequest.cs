

using AFF_BE.Core.Models.Common;

namespace AFF_BE.Core.Models.Identity.Role
{
    public class GetRoleByUserRequest:PagingRequest
    {
        public int UserId { get; set; }
    }
}
