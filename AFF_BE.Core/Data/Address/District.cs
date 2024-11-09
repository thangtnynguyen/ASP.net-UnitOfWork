using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Data.Address
{
    public partial class District
    {
        public District()
        {
            Wards = new HashSet<Ward>();
        }
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int? CityId { get; set; }
        public string? CityName { get; set; }
        public int? Status { get; set; }
        public int? IsDeleted { get; set; }
        public int? Version { get; set; }


        public virtual City? City { get; set; }
        public virtual ICollection<Ward> Wards { get; set; }


    }
}
