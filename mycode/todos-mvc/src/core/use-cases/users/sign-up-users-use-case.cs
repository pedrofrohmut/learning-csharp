using TodosMvc.Core.DataAccess;
using TodosMvc.Core.Services;

namespace TodosMvc.Core.UseCases.Users;

public class SignUpUserUseCase
{
    private readonly IUserDataAccess userDataAccess;
    private readonly IPasswordService passwordService;

    public SignUpUserUseCase(IUserDataAccess userDataAccess, IPasswordService passwordService)
    {
        this.userDataAccess = userDataAccess;
        this.passwordService = passwordService;
    }
}
