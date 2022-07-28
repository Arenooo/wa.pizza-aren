namespace WA.Pizza.Infrastructure.DTO.Order;

public record OrderDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public ICollection<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();
    public Core.Models.Order.OrderStatus Status { get; set; }
}