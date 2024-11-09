using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Data.Content
{
    public partial class Product : EntityBase<int>
    {
        public Product()
        {
            ProductImages = new HashSet<ProductImage>();
            ProductVariants = new HashSet<ProductVariant>();
        }
        public string? Code { get; set; }
        public string? Sku { get; set; }
        public string? Barcode { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public decimal? SellingPrice { get; set; }
        public decimal? ImportPrice { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? Mass { get; set; }
        public string? Content { get; set; }
        public string? LinkVideo { get; set; }
        public int? BrandId { get; set; }
        public int? ViewCounts { get; set; }
        public int? SellCounts { get; set; }
        public decimal? Width { get; set; }
        public decimal? Hight { get; set; }
        public decimal? Length { get; set; }
        public decimal? Weight { get; set; }
        public string? Origin { get; set; }
        public string? Tag { get; set; }
        public int? Status { get; set; }
        public int? Version { get; set; }
        public string? BrandName { get; set; }
        public string? CategoryName { get; set; }
        public string? UnitName { get; set; }
        public int? NumberDay { get; set; }
        public int? Warning { get; set; }
        public int? ProductType { get; set; }
        public virtual ProductCategory? Category { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductVariant> ProductVariants { get; set; }

    }
}
