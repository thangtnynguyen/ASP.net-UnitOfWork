using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.Brand
{
    public class UpdateBrandRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? UpdateAt
        {
            get
            {
                return DateTime.Now;
            }
        }
        public string? CreatedBy { get; set; }
        public string? CreateName { get; set; }
        public int? Version { get; set; }
    }
}
