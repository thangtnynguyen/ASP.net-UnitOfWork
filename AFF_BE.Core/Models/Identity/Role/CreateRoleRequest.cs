using System.Text.Json.Serialization;

namespace AFF_BE.Core.Models.Identity.Role
{
    public class CreateRoleRequest
    {

        public string? Name { get; set; }

        public string? Description { get; set; }

        [JsonIgnore]
        public string? NormalizedName {  get; set; }

        public List<int>? PermissionIds { get; set; }



    }
}
