using System.ComponentModel.DataAnnotations;

namespace Sneaker_Shop_API.Dto
{
    public class CreateSneakerDto
    {
        [Required]
        public string Model { get; set; } = string.Empty;
        [Required]
        public string[] Colors { get; set; } = new string[] { };
        [Required]
        public double Price { get; set; }
        public bool? InStock { get; set; } = true;
    }
}