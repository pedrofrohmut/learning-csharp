using System.Threading.Tasks;
using Shareposts.Core.Services;

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
        throw new System.NotImplementedException();
    }
}
