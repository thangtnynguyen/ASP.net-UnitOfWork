using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Auth
{
    public class LoginResult
    {
        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? Expiration { get; set; }
    }

}
