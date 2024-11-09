using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Data.Payment;
using AFF_BE.Core.Models.Identity.User;

namespace AFF_BE.Core.Models.Payment.Order;

public class OrderDto
{
    public int Id { get; set; }
    public string OrderTrackingNumber { get; set; }
    public string PhoneNumber { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string ShippingAddress { get; set; }
    public string Note { get; set; }
    public int PaymentAccountReceiptId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; }
    public int TotalQuantity { get; set; }
    public OrderUserDto User { get; set; }

}

