namespace BuberDinner.Infrastructure.Authentication;

public class JwtSettings
{
    public string Secret { get; init; } = "";
    public int ExpirationInMinutes { get; init; }
    public string Issuer { get; init; } = "";
    public string Audience { get; init; } = "";
}
