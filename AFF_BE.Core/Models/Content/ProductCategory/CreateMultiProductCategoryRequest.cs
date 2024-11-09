using AFF_BE.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.ProductCategory
{

    public class CreateMultiProductCategoryRequest
    {
        private string? des;
        private string _name;
        private int? _isDeleted;
        private int? _version;

        public string? Code
        {
            get
            {
                return StringHelper.GenerateRandomCode(8);
            }
        }
        public string Name
        {
            get => _name;
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
                if (value.Length > 500) { throw new ArgumentException("Maximum length for Product Description is 500 characters"); }
                des = value;
            }
        }
        public CreateMultiProductCategoryRequest? Parent { get; set; }
        public int? CollectionId { get; set; }
        public int? IsDeleted { get => 0; private set => _isDeleted = value; }
        public int? Version { get => 1; private set => _version = value; }
        public DateTime? CreatedAt
        {
            get => DateTime.Now;
        }
        public CreateMultiProductCategoryRequest() { }
        public CreateMultiProductCategoryRequest(CreateMultiProductCategoryRequest cate)
        {
            Name = cate.Name;
            if (cate.Description != null) { Description = cate.Description ?? ""; }
            if (cate.Parent != null) { Parent = cate.Parent; }
            if (cate.CollectionId != null) { CollectionId = cate.CollectionId ?? 0; }
            IsDeleted = cate.IsDeleted;
            Version = cate.Version;
        }
        public void Reverse()
        {
            if (this.Parent == null)
            {
                return;
            }
            CreateMultiProductCategoryRequest? _next = this;
            CreateMultiProductCategoryRequest? head = null;
            CreateMultiProductCategoryRequest? _befo = null;
            while (_next != null)
            {
                _befo = head;
                head = new CreateMultiProductCategoryRequest(_next);
                head.Parent = _befo;
                _next = _next.Parent;
            }
            this.Name = head.Name;
            if (Description != null) { this.Description = head.Description ?? ""; }
            if (Parent != null) { this.Parent = head.Parent; }
            if (CollectionId != null) { this.CollectionId = head.CollectionId ?? 0; }
            this.IsDeleted = head.IsDeleted;
            this.Version = head.Version;
        }
    }
}
