using System.Threading.Tasks;
using Shareposts.Core.Services;

namespace Shareposts.Services;

public class PasswordService : IPasswordService
{
    public Task<string> HashPassword(string password)
    {
        throw new System.NotImplementedException();
    }
}
