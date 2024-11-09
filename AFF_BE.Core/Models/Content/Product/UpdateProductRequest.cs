using AFF_BE.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.Product
{
    public class UpdateProductRequest
    {
        private string _name;
        private string? _description;
        private decimal? _width;
        private decimal? _height;
        private decimal? _length;
        private decimal? _weight;
        private decimal _sellingPrice;
        private decimal _importPrice;
        private int? _status;
        private int? _productType;

        public int Id { get; set; }
        public string Name
        {
            get => StringHelper.FormatWord(_name);
            set => _name = (value.Length <= 100) ? value : throw new ValidationException("Name cannot be null and must be less than or equal to 100 characters.");
        }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public decimal SellingPrice
        {
            get { return _sellingPrice; }
            set
            {
                if (value < 0) { throw new ArgumentOutOfRangeException("SellingPrice cannot be less than 0."); }
                _sellingPrice = value;
            }
        }
        public decimal ImportPrice
        {
            get { return _importPrice; }
            set
            {
                if (value < 0) { throw new ArgumentOutOfRangeException("ImportPrice cannot be less than 0."); }
                _importPrice = value;
            }
        }
        public string? Content { get; set; }
        public int? BrandId { get; set; }
        public int? WarrantyPolicyId { get; set; }
        public int? CollectionId { get; set; }
        public string? UnitName { get; set; }
        public int? NumberDay { get; set; }
        public int? Warning { get; set; }
        public decimal? Width
        {
            get { return _width; }
            set
            {
                if (value.HasValue && value < 0) { throw new ArgumentOutOfRangeException("Width cannot be less than 0."); }
                _width = value;
            }
        }

        public decimal? Hight
        {
            get { return _height; }
            set
            {
                if (value.HasValue && value < 0) { throw new ArgumentOutOfRangeException("Height cannot be less than 0."); }
                _height = value;
            }
        }

        public decimal? Length
        {
            get { return _length; }
            set
            {
                if (value.HasValue && value < 0) { throw new ArgumentOutOfRangeException("Length cannot be less than 0."); }
                _length = value;
            }
        }

        public decimal? Weight
        {
            get { return _weight; }
            set
            {
                if (value.HasValue && value < 0) { throw new ArgumentOutOfRangeException("Weight cannot be less than 0."); }
                _weight = value;
            }
        }
        public string? Origin { get; set; }
        public string? Tag { get; set; }
        public int? Status
        {
            get { return _status; }
            set
            {
                if (value == 1 || value == 0)
                {
                    _status = value;
                }
                else
                {
                    throw new ValidationException("Status equal 0 or 1");
                }
            }
        }
        public int? ProductType { get; set; }


        public int? Version { get; set; }
    }
}
