
using AFF_BE.Core.Models.Common;

namespace AFF_BE.Core.Models.Identity.Permission
{
    public class GetPermissionRequest:PagingRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? DisplayName { get; set; }


    }
}
