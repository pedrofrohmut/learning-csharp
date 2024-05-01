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

    public Task<bool> ValidateToken(string token)
    {
        var signinKey = SymmetricJwk.FromBase64Url(this.secret);

        var policy =
            new TokenValidationPolicyBuilder()
                .RequireSignatureByDefault(signinKey, SignatureAlgorithm.HS256)
                .RequireClaim("userId")
                .RequireClaim("exp")
                .Build();

        var ok = Jwt.TryParse(token, policy, out var decoded);

        // System.Console.WriteLine("Decoded token: " + decoded);
        // System.Console.WriteLine("Decoded token payload: " + decoded.Payload);
        // System.Console.WriteLine("Decoded token userId: " + decoded.Payload["userId"]);

        // Don't forget to dispose decoded or you get GC problems
        decoded.Dispose();

        return Task.FromResult(ok);
    }
}
