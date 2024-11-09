namespace AFF_BE.Core.Models.Commission;

public class IndirectCommissionDto
{
    public int Id { get; init; } 
    public int OrderId { get; set; }

    public string Code { get; set; }

    public double Price { get; set; }
}