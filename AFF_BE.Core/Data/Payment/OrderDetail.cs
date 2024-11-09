using AFF_BE.Core.Data.Content;

namespace AFF_BE.Core.Data.Payment;

public class OrderDetail:EntityBase<int>
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }
    public int Quantity { get; set; } 
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public virtual Product Product { get; set; }
    public virtual Order Order { get; set; }
}
