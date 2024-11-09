using AFF_BE.Core.Helpers;
using System.ComponentModel.DataAnnotations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.ProductVariant
{
    public class CreateProductVariantRequest
    {

        private decimal? _price;
        private int? _quantity;
        private int? _status;

        public string? Code
        {
            get
            {
                return StringHelper.GenerateRandomCode(8);
            }
        }
        public string? Barcode { get; set; }
        public string? Sku { get; set; }
        public long? ProductId { get; }
        public decimal? Price
        {
            get { return _price; }
            set
            {
                if (value.HasValue && value < 0) { throw new ArgumentOutOfRangeException("Price cannot be less than 0."); }
                _price = value;
            }
        }
        public decimal? Quantity { get; set; }

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
        public int? IsDeleted { get => 0; }
        public int? Version { get => 1; }
        public string? Propeties1 { get; set; }
        public string? ValuePropeties1 { get; set; }
        public string? Propeties2 { get; set; }
        public string? ValuePropeties2 { get; set; }
        public string? Base64_FileImage { get; set; }
        public DateTime? CreatedAt { get => DateTime.Now; }
        public DateTime? UpdatedAt { get => DateTime.Now; }


    }
}
