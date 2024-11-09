using AFF_BE.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.Product
{
    public class SearchProductByKeyWord:PagingRequest
    {

     
        public string? KeyWord { get; set; }
        public int? CategoryId { get; set; }
        public decimal? StartPrice { get; set; }
        public decimal? EndPrice { get; set; }

        public long? TotalQuantity { get; set; } = null;


    }

}
