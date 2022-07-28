namespace WA.Pizza.Infrastructure.DTO.Order;

public record OrderItemDTO()
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int OrderId { get; set; }
}