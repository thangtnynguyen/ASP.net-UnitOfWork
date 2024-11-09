using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.ProductImage
{
    public class CreateProductImageRequest
    {
        public int ProductId { get; set; }

        public string base64_image { get; set; }

    }
}
