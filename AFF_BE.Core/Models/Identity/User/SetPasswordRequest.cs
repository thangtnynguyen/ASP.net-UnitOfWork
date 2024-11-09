using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Identity.User
{
    public class SetPasswordRequest
    {
        public string Otp { get; set; }

        public string? Email { get; set; }

        public string NewPassword { get; set; }
    }
}
