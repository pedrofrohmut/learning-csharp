using TodosMvc.Core.Services;

namespace TodosMvc.Services;

public class PasswordService : IPasswordService
{
    public Task<string> HashPassword(string password)
    {
        throw new NotImplementedException();
    }
}
