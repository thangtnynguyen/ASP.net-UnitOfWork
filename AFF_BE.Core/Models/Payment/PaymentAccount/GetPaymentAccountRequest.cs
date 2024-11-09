using AFF_BE.Core.Models.Common;

namespace AFF_BE.Core.Models.Payment;

public class GetPaymentAccountRequest:PagingRequest
{
    public string? BankCode { get; set; } 
    public string? BankName { get; set; } 

}