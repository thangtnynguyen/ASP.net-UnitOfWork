using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Auth
{
    public class VerifyMailOtpRegisterRequest
    {

        public string Email { get; set; }

        public string Otp { get; set; }

    }
}
