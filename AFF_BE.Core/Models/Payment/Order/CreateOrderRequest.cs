using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Data.Payment;

namespace AFF_BE.Core.Models.Payment.Order;

public class CreateOrderRequest
{
    public string PhoneNumber { get; set; }
    public string ShippingAddress { get; set; }
    public string Note { get; set; }
    public int PaymentAccountReceiptId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }

    public List<CreateOrderDetailRequest> OrderDetails { get; set; }
}