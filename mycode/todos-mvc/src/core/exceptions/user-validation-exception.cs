namespace TodosMvc.Core.Exceptions;

public class UserValidationException : Exception
{
    public UserValidationException(string? message) : base(message) {}
}
