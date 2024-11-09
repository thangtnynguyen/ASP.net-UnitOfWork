using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Models.Content.ProductImage;

namespace AFF_BE.Core.Models.Payment.Order;

public class OrderProductDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public decimal? SellingPrice { get; set; }
    public string? Content { get; set; }
    public List<GetProductImagesManagementRequest> ProductImages { get; set; }


}