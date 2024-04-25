using System.Threading.Tasks;

namespace Shareposts.Core.Services;

public interface IPasswordService
{
    Task<string> HashPassword(string password);
    Task<bool> VerifyPasswordAndHash(string password, string passwordHash);
}
