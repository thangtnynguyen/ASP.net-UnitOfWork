using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Data.Content
{
    public partial class News : EntityBase<int>
    { 
        public string? Title { get; set; }
        public string? SubDescription { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public bool? IsActive { get; set; } = true;

    }
}
