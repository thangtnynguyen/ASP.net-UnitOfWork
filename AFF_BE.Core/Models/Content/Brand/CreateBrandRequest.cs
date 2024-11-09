using AFF_BE.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.Brand
{
    public class CreateBrandRequest
    {


        public string? Code { get => StringHelper.GenerateRandomCode(8); }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Version { get => 1; }
        public DateTime? CreatedAt
        {
            get
            {
                return DateTime.Now;
            }
        }
        public DateTime? UpdatedAt { get => DateTime.Now; }

    }
}
