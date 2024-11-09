﻿using Microsoft.AspNetCore.Identity;

namespace AFF_BE.Core.Models.Commission;

public class GetCommissionByUserDto
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? AvatarUrl { get; set; }

    public string? CitizenIdentification { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? ShippingAddress { get; set; }

    public string? BankAccountNumber { get; set; }

    public string? BankName { get; set; }

    public string? PersonalTaxCode { get; set; }

    public string? ReferralCode { get; set; }
    public string? Address { get; set; }

    public int? CityId { get; set; }

    public string? CityName { get; set; }

    public int? DistrictId { get; set; }

    public string? DistrictName { get; set; }

    public int? WardId { get; set; }

    public string? WardName { get; set; }
    public decimal? TotalDirectCommissions { get; set; }
    public decimal? TotalIndirectCommissions { get; set; }
        
    public DirectCommissionDto? DirectCommission{ get; set; } 
    public List<IndirectCommissionDto>? IndirectCommissions{ get; set; } 

}