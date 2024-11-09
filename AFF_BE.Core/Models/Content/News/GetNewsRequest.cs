using AFF_BE.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.News
{
    public class GetNewsRequest : PagingRequest
    {
        public string? Title { get; set; }
        public string? SubDescription { get; set; }
        public string? Content { get; set; }
    }
}
