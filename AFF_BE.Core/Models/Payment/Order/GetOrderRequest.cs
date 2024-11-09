using AFF_BE.Core.Models.Common;

namespace AFF_BE.Core.Data.Payment;

public class GetOrderRequest:PagingRequest
{
    public string? OrderTrackingNumber { get; set; }
    public OrderStatus? OrderStatus { get; set; }
    public int? PaymentAccountReceiptId { get; set; }
    public string? UserName { get; set; }
    public DateTime? FromDate { get; set; } 
    public DateTime? ToDate { get; set; } 
}