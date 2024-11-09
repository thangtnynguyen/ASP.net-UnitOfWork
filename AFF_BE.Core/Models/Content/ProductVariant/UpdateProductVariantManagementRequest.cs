using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.ProductVariant
{
    public class UpdateProductVariantManagementRequest
    {
        public int? Id { get; set; }
        public string? Code { get; set; }
        public string? Sku { get; set; }
        public string? Barcode { get; set; }
        public decimal? Price { get; set; }
        public int? ProductId { get; set; }
        public decimal? Quantity { get; set; }
        public string? Propeties1 { get; set; }
        public string? ValuePropeties1 { get; set; }
        public string? Propeties2 { get; set; }
        public string? ValuePropeties2 { get; set; }
        public string? Base64_FileImage { get; set; }
        public int? Status { get; set; }
        public int? IsDeleted { get; set; }
        public int? Version { get; set; }
        public string? LinkImage { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreateName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedName { get; set; }

    }
}
