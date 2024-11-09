using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace AFF_BE.Core.Data.Identity
{
    public class Role : IdentityRole<int>
    {
        public string? Description { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        [JsonIgnore]
        public virtual List<RolePermission> RolePermissions { get; set; }
    }
}
