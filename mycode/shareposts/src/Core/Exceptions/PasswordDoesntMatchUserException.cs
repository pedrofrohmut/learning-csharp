using System;

namespace Shareposts.Core.Exceptions;

public class PasswordDoesntMatchUserException : Exception
{
    public PasswordDoesntMatchUserException() : base(
        "Password does not user password hash") {}
}
