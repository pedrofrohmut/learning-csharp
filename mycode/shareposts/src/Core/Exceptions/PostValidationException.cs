using System;

namespace Shareposts.Core.Exceptions;

public class PostValidationException : Exception
{
    public PostValidationException(string? message) : base(message) {}
}
