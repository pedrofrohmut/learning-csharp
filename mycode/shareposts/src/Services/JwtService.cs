using System.Threading.Tasks;
using Shareposts.Core.Services;
using JsonWebToken;

namespace Shareposts.Services;

public class JwtService : IJwtService
{
    private readonly string secret;

    public JwtService(string secret)
    {
        this.secret = secret;
    }

    public Task<string> CreateJwt(string userId)
    {
        var signingKey = SymmetricJwk.FromBase64Url(this.secret);

        var descriptor = new JwsDescriptor(signingKey, SignatureAlgorithm.HS256) {
            Payload = new JwtPayload() {
                // Issued at
                { "iat", EpochTime.UtcNow },
                // Expiration date
                { "exp", EpochTime.UtcNow + EpochTime.OneDay },
                // User indentifier
                { "userId", userId },
            }
        };

        var token = new JwtWriter().WriteTokenString(descriptor);

        return Task.FromResult(token);
    }

    private TokenValidationPolicy GetValidationPolicy()
    {
        var signinKey = SymmetricJwk.FromBase64Url(this.secret);

        return new TokenValidationPolicyBuilder()
            .RequireSignatureByDefault(signinKey, SignatureAlgorithm.HS256)
            .RequireClaim("userId")
            .RequireClaim("exp")
            .Build();
    }


    public Task<(bool, string)> ValidateToken(string token)
    {
        var policy = GetValidationPolicy();
        var ok = Jwt.TryParse(token, policy, out var decoded);
        if (! ok) return Task.FromResult((false, ""));

        string? userId = decoded.Payload?["userId"].ToString();
        decoded.Dispose(); // Don't forget to dispose decoded or you get GC problems

        if (userId == null) {
            return Task.FromResult((false, ""));
        }
        return Task.FromResult((true, userId));
    }

    public Task<string?> GetUserIdFromToken(string token)
    {
        var policy = GetValidationPolicy();
        var ok = Jwt.TryParse(token, policy, out var decoded);
        if (! ok) return Task.FromResult<string?>(null);

        string? userId = decoded.Payload?["userId"].ToString();
        decoded.Dispose(); // Don't forget to dispose decoded or you get GC problems

        return Task.FromResult(userId);
    }
}
