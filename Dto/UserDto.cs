using System.ComponentModel.DataAnnotations;

namespace Sneaker_Shop_API.Dto;

public record UserRegisterDto(
    [Required] string FirstName,
    [Required] string LastName,
    [Required, EmailAddress] string Email,
    [Required] string Password,
    [Required, Phone] string Phone,
    [Required] string Address,
    [Required] string Role
);

public record UserLoginDto(
    [Required, EmailAddress] string Email,
    [Required] string Password
);