namespace DeskMarket.Models.Product;

public class ProductCartViewModel
{
    public int Id { get; set; }

    public required string ProductName { get; set; }

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }
}
