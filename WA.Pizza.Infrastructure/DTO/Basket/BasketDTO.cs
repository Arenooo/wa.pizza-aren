namespace WA.Pizza.Infrastructure.DTO.Basket;

public record BasketDTO
{
    public int Id { get; set; }
    public ICollection<BasketItemDTO> Items { get; set; } = new List<BasketItemDTO>();
}