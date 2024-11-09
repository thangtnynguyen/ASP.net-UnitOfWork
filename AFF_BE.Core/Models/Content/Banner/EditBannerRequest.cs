﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AFF_BE.Core.Models.Content.Banner
{
    public class EditBannerRequest
    {
        public int Id { get; set; }

        public string? Place { get; set; }

        public string? Type { get; set; }

        [JsonIgnore]
        public string? Image { get; set; }

        public IFormFile? ImageFile { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Alt { get; set; }

        public string? CtaTitle { get; set; }

        public string? LinkTo { get; set; }

        public string? Properties { get; set; }

        public bool? IsBlank { get; set; }

        public int? Priority { get; set; }

        public DateTime? Expired { get; set; }
    }


}
