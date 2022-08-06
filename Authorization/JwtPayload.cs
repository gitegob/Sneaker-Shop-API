namespace Sneaker_Shop_API.Authorization;

public class JwtPayload
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}