using AFF_BE.Core.Helpers;
using AFF_BE.Core.Models.Content.ProductVariant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.Product
{
    public class CreateProductWithFileRequest
    {

        private string _name;
        private string? _description;
        private string? _content;
        private decimal? _width;
        private decimal? _hight;
        private decimal? _length;
        private decimal? _weight;
        private decimal? _sellingprice;
        private decimal? _importprice;
        private decimal? _totalQuantity;
        private int? _status;
        private int? _productType;
        public string? Code { get { return StringHelper.GenerateRandomCode(8); } }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public decimal? SellingPrice
        {
            get { return _sellingprice; }
            set
            {
                if (value == null) { _sellingprice = 0; }

                if (value < 0) { throw new ArgumentOutOfRangeException("SellingPrice cannot be less than 0."); }
                _sellingprice = value;
            }
        }
        public decimal? ImportPrice
        {
            get { return _importprice; }
            set
            {
                if (value == null) { _importprice = 0; }

                if (value < 0) { throw new ArgumentOutOfRangeException("ImportPrice cannot be less than 0."); }
                _importprice = value;
            }
        }
        public string? Content
        {
            get { return _content; }
            set
            {
                if (string.IsNullOrEmpty(value)) { return; }
                if (value.Length > 500)
                {
                    throw new ArgumentOutOfRangeException("Content cannot exceed 500 characters.");
                }
                _content = value;
            }
        }
        public string[]? Base64_FileIamges { get; set; }
        public string? Base64_FileVideo { get; set; }
        public int? BrandId { get; set; }
        public string? UnitName { get; set; }
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
        public decimal? TotalQuantity
        {
            get { return _totalQuantity; }
            set
            {
                if (value == null) { _totalQuantity = 0; }
                if (value < 0) { throw new ArgumentOutOfRangeException("TotalQuantity cannot be less than 0."); }
                _totalQuantity = value;
            }
        }
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
        public string? Origin { get; set; }
        public string? Tag { get; set; }
        public int? Status
        {
            get
            {
                if (_status == null)
                {
                    return 1;
                }
                return _status;
            }
            set
            {
                if (value < 0 || value > 1) { throw new ValidationException("Status must equal 0 or 1."); }
                _status = value;
            }
        }
        public int? ProductType
        {
            get
            {
                if (_productType == null)
                {
                    return 1;
                }
                return _productType;
            }
            set
            {
                if (value < 0 || value > 1) { throw new ValidationException("ProductType must equal 0 or 1."); }
                _productType = value;
            }
        }
        public decimal? Mass { get; set; }
        public int? Version { get => 1; }
        public int? ViewCounts { get => 0; }
        public int? SellCounts { get => 0; }
        public DateTime? CreatedAt { get => DateTime.Now; }
        public DateTime? UpdatedAt { get => DateTime.Now; }
        public string? Barcode { get; set; }
        public string? Sku { get; set; }
        public int? NumberDay { get; set; }
        public int? Warning { get; set; }
        public List<CreateProductVariantRequest>? productVariants { get; set; }

    }

}
