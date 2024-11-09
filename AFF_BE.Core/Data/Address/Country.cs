using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Data.Address
{
    public partial class Country 
    {

        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int? Status { get; set; }
        public int? IsDeleted { get; set; }
        public int? Version { get; set; }


        public virtual ICollection<City> Cities { get; set; }
    }


}
