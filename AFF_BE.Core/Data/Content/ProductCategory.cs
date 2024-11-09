using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Data.Content
{
    public partial class ProductCategory : EntityBase<int>
    {

        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public int? Version { get; set; }
        public int? Level { get; set; }
        public string? ParentName { get; set; }

        public int? Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }


}
