using System.Threading.Tasks;

namespace Shareposts.Core.Services;

public interface IJwtService
{
    Task<string> CreateJwt(string userId);
}
