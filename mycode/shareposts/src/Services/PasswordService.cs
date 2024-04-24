using System.Threading.Tasks;
using Shareposts.Core.Services;
using BC = BCrypt.Net.BCrypt;

namespace Shareposts.Services;

public class PasswordService : IPasswordService
{
    public Task<string> HashPassword(string password)
    {
        return Task.FromResult(BC.HashPassword(password));
    }
}
