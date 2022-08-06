using System.ComponentModel.DataAnnotations;

namespace Sneaker_Shop_API.Models;

public abstract class BaseEntity
{
    [Key, Required]
    public int Id { get; set; }
}