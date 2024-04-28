using System;

namespace Shareposts.Core.Exceptions;

public class EntityValidationException : Exception
{
    public EntityValidationException(string? message) : base(message) {}
}
