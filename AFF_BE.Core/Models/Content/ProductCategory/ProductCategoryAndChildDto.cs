using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.ProductCategory
{

    public class ProductCategoryAndChildDto
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public string? ParentName { get; set; }
        public int? CollectionId { get; set; }
        public string? CollectionName { get; set; }
        public int? IsDeleted { get; set; }
        public int? Status { get; set; }
        public int? Version { get; set; }
        public int? Level { get; set; }
        public List<ProductCategoryAndChildDto>? Children { get; set; }
        public ProductCategoryAndChildDto()
        {
            Children = new List<ProductCategoryAndChildDto>();
        }
    }
}
