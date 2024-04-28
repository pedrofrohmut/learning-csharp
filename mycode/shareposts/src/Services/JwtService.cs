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
                // Predefined names claims
                { JwtClaimNames.Iat, EpochTime.UtcNow }, // Issued at
                { JwtClaimNames.Exp, EpochTime.UtcNow + EpochTime.OneDay }, // Expiration date
                // Custom claims
                { "userId", userId },
            }
        };

        var token = new JwtWriter().WriteTokenString(descriptor);

        return Task.FromResult(token);
    }
}
