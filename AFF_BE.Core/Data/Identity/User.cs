using AFF_BE.Core.Data.Address;
using AFF_BE.Core.Data.Commission;
using Microsoft.AspNetCore.Identity;

namespace AFF_BE.Core.Data.Identity
{
    public class User : IdentityUser<int>
    {
        public string? Name { get; set; }

        public string? AvatarUrl { get; set; }

        public string? CitizenIdentification { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? ShippingAddress { get; set; }

        public string? BankAccountNumber { get; set; }

        public string? BankName { get; set; }

        public string? PersonalTaxCode { get; set; }

        public string? ReferralCode { get; set; }

        public int? Sex { get; set; }

        public string? Address { get; set; }

        public int? CityId { get; set; }

        public string? CityName { get; set; }

        public int? DistrictId { get; set; }

        public string? DistrictName { get; set; }

        public int? WardId { get; set; }

        public string? WardName { get; set; }

        public DateTime? BirthDay { get; set; }

        public bool? Status { get; set; } = true;

        public bool? IsDeleted { get; set; }=false;

        public string? RefreshToken { get; set; }

        public bool? IsRefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        #region Relationship
        public virtual List<UserRole>? UserRoles { get; set; }

        public virtual DirectCommission DirectCommission { get; set; }
        public virtual List<IndirectCommission> IndirectCommissions  { get; set; }

        public virtual District? District { get; set; }

        public virtual City? City { get; set; }

        public virtual Ward? Ward { get; set; }

        #endregion
    }
}
