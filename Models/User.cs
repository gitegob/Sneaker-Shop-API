using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sneaker_Shop_API.Models;

[Table("users")]
public class User : BaseEntity
{
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    [JsonIgnore]
    public string Password { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Address { get; set; } = string.Empty;
    public string Role { get; set; } = "CLIENT";
}