using AFF_BE.Core.Data;

namespace AFF_BE.Core.Models.Payment;

public class PaymentAccount:EntityBase<int>
{
    public string BankCode { get; set; } 
    public string BankName { get; set; } 
    public string AccountNumber { get; set; }
    public string AccountName { get; set; }
    public string Description { get; set; }
}