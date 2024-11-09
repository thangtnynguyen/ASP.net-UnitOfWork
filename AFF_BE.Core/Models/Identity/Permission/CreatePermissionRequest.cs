using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Identity.Permission
{
    public class CreatePermissionRequest
    {

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public int? ParentPermissionId { get; set; }

        public string Description { get; set; }


    }
}
