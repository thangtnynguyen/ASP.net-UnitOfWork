using AFF_BE.Core.Models.Content.ProductImage;
using AFF_BE.Core.Models.Content.ProductVariant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.Product
{
    public class ProductManagementDto
    {
        public static int TotalRecordsCount = 0;
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Sku { get; set; }
        public string? Barcode { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public decimal? SellingPrice { get; set; }
        public decimal? ImportPrice { get; set; }
        public decimal? TotalQuantity { get; set; }//luu y tny 
        public decimal? Mass { get; set; }
        public string? Content { get; set; }
        public string? LinkVideo { get; set; }
        public int? BrandId { get; set; }
        public int? CollectionId { get; set; }
        public int? ViewCounts { get; set; }
        public int? SellCounts { get; set; }
        public decimal? Width { get; set; }
        public decimal? Hight { get; set; }
        public decimal? Length { get; set; }
        public decimal? Weight { get; set; }
        public string? Origin { get; set; }
        public int? StoreId { get; set; }
        public string? Tag { get; set; }
        public int? Status { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Version { get; set; }
        public string? BrandName { get; set; }
        public string? CategoryName { get; set; }
        public string? WarrantyPolicyName { get; set; }
        public string? CollectionName { get; set; }
        public string? StoreName { get; set; }
        public string? UnitName { get; set; }
        public int? NumberDay { get; set; }
        public int? Warning { get; set; }
        public int? ProductType { get; set; }
        public int? WarrantyPolicyId { get; set; }
        public int? CreatedBy { get; set; }
        public string? CreateName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public string? UpdatedName { get; set; }

        public long? QuantityAvaiable { get; set; }

        public List<ProductVariantManagementDto> ProductVariants { get; set; }
        public List<GetProductImagesManagementRequest> ProductImages { get; set; }
        public ProductManagementDto() { }

    }
}
