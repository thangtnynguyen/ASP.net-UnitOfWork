using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.Banner
{
    public class BannerDto
    {
        public int Id { get; set; }

        public string Place { get; set; }

        public string Type { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Alt { get; set; }

        public string CtaTitle { get; set; }

        public string LinkTo { get; set; }

        public string Properties { get; set; }

        public bool IsBlank { get; set; }

        public int Priority { get; set; }

        public DateTime Expired { get; set; }
    }


}
