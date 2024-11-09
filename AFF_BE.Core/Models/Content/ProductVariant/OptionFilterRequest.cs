using AFF_BE.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.ProductVariant
{
    public class OptionFilterRequest:PagingRequest
    {
        public string? ProductName { get; set; }
        public bool? IsOutOfStock { get; set; }
        public string? Barcode { get; set; }
    }
}
