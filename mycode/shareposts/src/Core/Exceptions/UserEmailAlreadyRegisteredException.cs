using System;

namespace Shareposts.Core.Exceptions;

public class UserEmailAlreadyRegisteredException : Exception
{
    public UserEmailAlreadyRegisteredException() : base(
        "User with this e-mail is already registered and e-mail must be unique") {}
}
