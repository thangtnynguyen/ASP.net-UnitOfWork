using AFF_BE.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.ProductCategory
{

    public class CreateProductCategoryRequest
    {
        private string? des;
        private string _name;

        public string? Code { get => StringHelper.GenerateRandomCode(8); }
        public string Name
        {
            get => StringHelper.FormatWord(_name);
            set
            {
                if (value == null) { return; }
                if (value.Length < 3 || value.Length > 100) { throw new ValidationException("The name must be between 3 and 100 characters in length"); }
                _name = value;
            }
        }
        public string? Description
        {
            get => des;
            set
            {
                if (value == null) { return; }
                if (value.Length > 500) { throw new ValidationException("Maximum length for Product Description is 500 characters"); }
                des = value;
            }
        }
        public int? ParentId { get; set; }
        public int? Status { get; set; }
        public int? Version { get => 1; }
        public DateTime? CreatedAt
        {
            get
            {
                return DateTime.Now;
            }
        }
        public DateTime? UpdatedAt { get => DateTime.Now; }

    }
}
