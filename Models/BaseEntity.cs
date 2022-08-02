using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sneaker_Shop_API.Models;

public abstract class BaseEntity
{
    [Key, Required]
    public int Id { get; set; }
}