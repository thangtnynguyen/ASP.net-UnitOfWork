using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.ProductVariant
{
    public class UpdateProductVariantRequest
    {
        private string _name;
        private decimal? _price;
        private int? _quantity;

        public string? Sku { get; set; }
        public string Name
        {
            get => _name;
            set => _name = (value.Length <= 100) ? value : throw new ValidationException("Name cannot be null and must be less than or equal to 100 characters.");
        }

        public decimal? Price
        {
            get { return _price; }
            set
            {
                if (value.HasValue && value < 0) { throw new ValidationException("Price cannot be less than 0."); }
                _price = value;
            }
        }
        public decimal? Quantity { get; set; }

        public int? Version
        { get; set; }
        public DateTime? UpdatedAt { get => DateTime.Now; }
        public IFormFile? FileImage { get; set; }
    }
}
