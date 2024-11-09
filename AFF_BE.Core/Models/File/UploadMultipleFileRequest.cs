using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.File
{
    public class UploadMultipleFileRequest
    {
        public List<IFormFile>? Files { get; set; }

    }
}
