using AFF_BE.Core.Data.Payment;

namespace AFF_BE.Core.Models.Payment.Order;

public class EditOrderStatus
{
    public int Id { get; set; }
    public OrderStatus OrderStatus { get; set; }
}