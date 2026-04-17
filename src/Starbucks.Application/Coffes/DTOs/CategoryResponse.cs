namespace Starbucks.Application.Coffes.DTOs;

public class CoffeResponse
{
    public Guid CoffeId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int CategoryId {get; set; }
}