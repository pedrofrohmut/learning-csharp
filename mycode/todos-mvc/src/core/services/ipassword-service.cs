
namespace TodosMvc.Core.Services;

public interface IPasswordService
{
    Task<string> HashPassword(string password);
    Task<bool> VerifyPassword(string password, string passwordHash);
}
