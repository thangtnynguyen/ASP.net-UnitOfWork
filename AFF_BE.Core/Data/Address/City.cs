using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Data.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Data.Address
{
    public partial class City :EntityBase<int>
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int? CountryId { get; set; }
        public int? Status { get; set; }
        public int? IsDeleted { get; set; }
        public int? Version { get; set; }

        public virtual Country? Country { get; set; }
        public virtual ICollection<District> Districts { get; set; }


    }
}
