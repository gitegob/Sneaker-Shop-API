using System.ComponentModel.DataAnnotations;
using Sneaker_Shop_API.Enums;

namespace Sneaker_Shop_API.Dto;

public record CreateUserDto(
    [Required] string FirstName,
    [Required] string LastName,
    [Required, EmailAddress] string Email,
    [Required] string Password,
    [Required, Phone] string Phone,
    [Required] string Address,
    [Required] string Role
);

public record UserSignupDto(
    [Required] string FirstName,
    [Required] string LastName,
    [Required, EmailAddress] string Email,
    [Required] string Password,
    [Required, Phone] string Phone,
    [Required] string Address
);
public record UserLoginDto(
    [Required, EmailAddress] string Email,
    [Required] string Password
);

public record ViewUserDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string Role
);