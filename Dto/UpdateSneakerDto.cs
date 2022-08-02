namespace Sneaker_Shop_API.Dto
{
    public class UpdateSneakerDto
    {
        public string? Model { get; set; }
        public string[]? Colors { get; set; }
        public double? Price { get; set; }
        public bool? InStock { get; set; }
    }
}