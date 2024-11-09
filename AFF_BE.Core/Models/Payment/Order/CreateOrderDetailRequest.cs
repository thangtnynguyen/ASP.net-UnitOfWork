using AFF_BE.Core.Models.Content.Product;

namespace AFF_BE.Core.Models.Payment.Order;

public class CreateOrderDetailRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; } 
    public decimal UnitPrice { get; set; }
}