namespace Sneaker_Shop_API.Dto
{
    public class CreateSneakerDto
    {
        public string? Model { get; set; }
        public string[]? Colors { get; set; } = new string[] { };
        public double? Price { get; set; }
        public bool? InStock { get; set; } = true;
    }
}