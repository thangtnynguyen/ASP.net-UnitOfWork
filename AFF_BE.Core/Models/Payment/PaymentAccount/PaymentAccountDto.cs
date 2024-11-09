namespace AFF_BE.Core.Models.Payment;

public class PaymentAccountDto
{
    public int Id { get; set; }
    public string BankCode { get; set; } 
    public string BankName { get; set; } 
    public string AccountNumber { get; set; }
    public string AccountName { get; set; }
    public string Description { get; set; }
}