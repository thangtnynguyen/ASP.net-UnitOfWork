using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Data.Identity
{
    public class Permission : EntityBase<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public int? ParentPermissionId { get; set; }

        public string? Description { get; set; }

        public Permission? ParentPermission { get; set; }

        public virtual List<RolePermission> RolePermissions { get; set; }
    }

}
