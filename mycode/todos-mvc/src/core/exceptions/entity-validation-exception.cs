namespace TodosMvc.Core.Exceptions;

public class EntityValidationException : Exception
{
    public EntityValidationException(string? message) : base(message) {}
}
