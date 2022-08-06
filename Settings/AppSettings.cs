namespace Sneaker_Shop_API.Settings;

public class AppSettings
{
    public string? AllowedHosts { get; init; }
    public ConnectionStrings? ConnectionStrings { get; init; }
    public Jwt? Jwt { get; init; }
};

public class ConnectionStrings
{
    public string? Default { get; init; }
};

public record Jwt
{
    public string? Key { get; init; }
};