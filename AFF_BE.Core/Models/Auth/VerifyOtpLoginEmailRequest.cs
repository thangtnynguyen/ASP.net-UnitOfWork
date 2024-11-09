namespace AFF_BE.Core.Models.Auth
{
    public class VerifyOtpLoginEmailRequest
    {
        public string? Name { get; set; }

        public string Email { get; set; }

        public string Otp { get; set; }

        public string? Password { get;set; }
    }
}
