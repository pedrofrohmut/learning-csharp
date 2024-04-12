namespace TodosMvc.Core.Exceptions;

public class GoalValidationException : Exception
{
    public GoalValidationException(string? message) : base(message) {}
}
