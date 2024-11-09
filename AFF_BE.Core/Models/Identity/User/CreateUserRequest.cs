using AFF_BE.Core.Data.Commission;
using AFF_BE.Core.Models.Commission;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AFF_BE.Core.Models.Identity.User
{
    public class CreateUserRequest
    {
        public string? Name { get; set; }

        public string? CitizenIdentification { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? ShippingAddress { get; set; }

        public string? BankAccountNumber { get; set; }

        public string? BankName { get; set; }

        public string? PersonalTaxCode { get; set; }

        public string? ReferralCode { get; set; }

        public IFormFile? AvatarFile { get; set; }


        [System.Text.Json.Serialization.JsonIgnore]
        public string? AvatarUrl { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Password { get; set; }

        public bool? Status { get; set; }

        public string? Address { get; set; }

        public List<string>? Roles { get; set; }

        public int? CityId { get; set; }

        public string? CityName { get; set; }

        public int? DistrictId { get; set; }

        public string? DistrictName { get; set; }

        public int? WardId { get; set; }

        public string? WardName { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public CreateDirectCommissionRequest? DirectCommission { get; set; }


    }
}
