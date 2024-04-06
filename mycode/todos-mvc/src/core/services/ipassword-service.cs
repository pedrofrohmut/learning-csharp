
namespace TodosMvc.Core.Services;

public interface IPasswordService
{
    Task<string> HashPassword(string password);
}
