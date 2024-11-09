using AFF_BE.Core.Models.Content.ProductImage;
using AFF_BE.Core.Models.Content.ProductVariant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.Product
{
    public class UpdateProductManagementRequest
    {
        private decimal? _width;
        private decimal? _hight;
        private decimal? _length;
        private decimal? _weight;
        private int? _totalQuantity;
        private int? _mass;
        private string? _description;
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Sku { get; set; }
        public string? Barcode { get; set; }
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public decimal? SellingPrice { get; set; }
        public decimal? ImportPrice { get; set; }
        public string? Content { get; set; }
        public string? LinkVideo { get; set; }
        public int? BrandId { get; set; }
        public int? WarrantyPolicyId { get; set; }
        public int? CollectionId { get; set; }
        public int? ViewCounts { get; set; }
        public int? SellCounts { get; set; }
        public string? Origin { get; set; }
        public int? StoreId { get; set; }
        public string? Tag { get; set; }
        public int? Status { get; set; }
        public int? ProductType { get; set; }
        public int? IsDeleted { get; set; }
        public int? Version { get; set; }
        public string? BrandName { get; set; }
        public string? CategoryName { get; set; }
        public string? CollectionName { get; set; }
        public string? WarrantyPolicyName { get; set; }
        public string? StoreName { get; set; }
        public string? UnitName { get; set; }
        public int? NumberDay { get; set; }
        public int? Warning { get; set; }
        public string? Base64_FileVideo { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreateName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public decimal? Width
        {
            get { return _width; }
            set
            {
                if (value == null) { _width = 0; }
                if (value <= 0) { throw new ArgumentOutOfRangeException("Width cannot be less than 0."); }
                _width = value;
            }
        }
        public decimal? Mass { get; set; }

        public decimal? TotalQuantity { get; set; }
        public decimal? Hight
        {
            get { return _hight; }
            set
            {
                if (value == null) { _hight = 0; }

                if (value <= 0) { throw new ArgumentOutOfRangeException("Hight cannot be less than 0."); }
                _hight = value;
            }
        }

        public decimal? Length
        {
            get { return _length; }
            set
            {
                if (value == null) { _length = 0; }
                if (value <= 0) { throw new ArgumentOutOfRangeException("Length cannot be less than 0."); }
                _length = value;
            }
        }

        public decimal? Weight
        {
            get { return _weight; }
            set
            {
                if (value == null) { _weight = 0; }
                if (value <= 0) { throw new ArgumentOutOfRangeException("Weight cannot be less than 0."); }
                _weight = value;
            }
        }
        public string? Description { get; set; }



        public List<UpdateProductVariantManagementRequest>? ProductVariants { get; set; }
        public List<UpdateProductImagesManagementRequest>? ProductImages { get; set; }
        public UpdateProductManagementRequest() { }
    }
}
