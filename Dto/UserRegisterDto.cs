using System.ComponentModel.DataAnnotations;

namespace Sneaker_Shop_API.Dto;

public class UserRegisterDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required, Phone]
    public string Phone { get; set; } = string.Empty;
    [Required]
    public string Address { get; set; } = string.Empty;
}