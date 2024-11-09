using AFF_BE.Core.Data;
using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Data.Identity;

namespace AFF_BE.Core.Data.Payment;

public class Order:EntityBase<int>
{
    public string OrderTrackingNumber { get; set; }
    public string PhoneNumber { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string ShippingAddress { get; set; }
    public string Note { get; set; } 
    public int PaymentAccountReceiptId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int TotalQuantity { get; set; }
    public decimal TotalAmount { get; set; }
    
    #region relasionship
    public virtual List<OrderDetail> OrderDetails { get; set; }
    public virtual User User { get; set; }

    #endregion
    
}

public enum OrderStatus
{
    Pending,
    Approved,
    Inprocess,
    Success,
    Cancelled,
}