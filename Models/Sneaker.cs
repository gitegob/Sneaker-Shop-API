using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sneaker_Shop_API.Models;

[Table("sneakers")]
public class Sneaker : BaseEntity
{
    public string? Model { get; set; } = string.Empty;
    public string[]? Colors { get; set; } = new string[] { };
    public double? Price { get; set; }
    public bool? InStock { get; set; } = true;
}