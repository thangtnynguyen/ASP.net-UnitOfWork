using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.ProductVariant
{
    public class FilterProductVariantRequest
    {
        public static int TotalRecordsCount = 0;

        public int ProductId { get; set; }
        public string? ProductCode { get; set; }
        public int ProductVariantId { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }
        public string? Unit { get; set; }
        public string? Properties1 { get; set; }
        public string? Properties2 { get; set; }
        public string? ValuePropeties1 { get; set; }
        public string? ValuePropeties2 { get; set; }
        public decimal Inventory { get; set; }
        public int? ProductType { get; set; }
        public decimal Price { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? Barcode { get; set; }
        public string? Sku { get; set; }

    }
}
