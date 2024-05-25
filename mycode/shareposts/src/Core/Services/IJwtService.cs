using System.Threading.Tasks;

namespace Shareposts.Core.Services;

public interface IJwtService
{
    Task<string> CreateJwt(string userId);
    Task<(bool, string)> ValidateToken(string token);
    Task<string?> GetUserIdFromToken(string token);
}
