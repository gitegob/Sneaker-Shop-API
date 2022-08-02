namespace Sneaker_Shop_API.Models;

public class Sneaker
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Model { get; set; }
    public string[]? Colors { get; set; } = new string[] { };
    public double? Price { get; set; }
    public bool? InStock { get; set; } = true;
}