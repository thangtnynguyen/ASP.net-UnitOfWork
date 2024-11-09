using System.ComponentModel.DataAnnotations;

namespace AFF_BE.Core.Models.Auth
{
    public class LoginEmailRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
