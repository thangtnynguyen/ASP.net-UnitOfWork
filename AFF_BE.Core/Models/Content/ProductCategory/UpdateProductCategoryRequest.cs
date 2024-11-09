using AFF_BE.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.ProductCategory
{

    public class UpdateProductCategoryRequest
    {
        private string? _name;
        private string? _description;
        private int? _parentId;
        private int? _version = 1;
        private int? _isDelete = 0;

        public int Id { get; set; }
        public string Name
        {
            get => StringHelper.FormatWord(_name);
            set
            {
                if (string.IsNullOrEmpty(value)) { return; }
                if (value.Length < 3 || value.Length > 100) { throw new ValidationException("The name must be between 3 and 100 characters in length"); }
                _name = value;
            }
        }
        public string? Description
        {
            get { return _description; }
            set
            {
                if (string.IsNullOrEmpty(value)) { return; }
                if (value.Length > 500) { throw new ValidationException("Maximum length for Product Description is 500 characters"); }
                _description = value;
            }
        }
        public int? ParentId
        {
            get { return _parentId; }
            set
            {
                if (value == null) { return; }
                if (value < 0) { throw new ValidationException("ParentId cannot be less than 0 "); }
                _parentId = value;
            }
        }
        public int? Version { get; set; }
        public int? IsDeleted
        {
            get { return _isDelete; }
        }
        public int? Status { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreateName { get; set; }

        public DateTime? UpdatedAt { get => DateTime.Now; }

    }

}
