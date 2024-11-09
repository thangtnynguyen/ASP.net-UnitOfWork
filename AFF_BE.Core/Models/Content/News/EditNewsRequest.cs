using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.News
{
    public class EditNewsRequest
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? SubDescription { get; set; }
        public string? Content { get; set; }

        [JsonIgnore]
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
