using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Models.Content.Product;

namespace AFF_BE.Core.Models.Payment.Order;

public class OrderDetailDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; } 
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public  OrderProductDto Product { get; set; }
}