using System;

namespace Shareposts.Core.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("User not found") {}
}
