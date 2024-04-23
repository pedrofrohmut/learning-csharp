using System.Threading.Tasks;
using Shareposts.Core.DataAccess;
using Shareposts.Core.Services;
using Shareposts.Core.Dtos.UseCases;

namespace Shareposts.Core.UseCases.Users;

public class SignUpUserUseCase
{
    private readonly IUserDataAccess userDataAccess;
    private readonly IPasswordService passwordService;

    public SignUpUserUseCase(IUserDataAccess userDataAccess, IPasswordService passwordService)
    {
        this.userDataAccess = userDataAccess;
        this.passwordService = passwordService;
    }

    public async Task Execute(CreateUserDto newUser)
    {
    }
}
