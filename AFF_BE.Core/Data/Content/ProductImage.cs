using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Data.Content
{
    public partial class ProductImage : EntityBase<int>
    {
        public string? Link { get; set; }
        public int? ProductId { get; set; }
        public int? IsDeleted { get; set; }
        public int? Version { get => 1; }
        public string? Code { get; set; }


        public virtual Product? Product { get; set; }
    }
}
