using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.Brand
{
    public class BrandDto
    {
        public int? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Version { get; set; }
        public int? StoreId { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreateName { get; set; }

    }
}
