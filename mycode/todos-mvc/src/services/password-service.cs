using TodosMvc.Core.Services;
using BCryptNet = BCrypt.Net.BCrypt;

namespace TodosMvc.Services;

public class PasswordService : IPasswordService
{
    public Task<string> HashPassword(string password)
    {
        var hash = BCryptNet.HashPassword(password);
        return Task.FromResult(hash);
    }
}
