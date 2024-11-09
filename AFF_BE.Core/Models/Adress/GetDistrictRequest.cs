using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Adress
{
    public class GetDistrictRequest
    {
        public string? Name { get; set; }
        public string? CityName { get; set; }
        public int? CityId { get; set; }
    }
}
