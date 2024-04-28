using Shareposts.Core.Exceptions;
using System;

namespace Shareposts.Core.Entities;

public static class MainEntity
{
    public static void ValidateId(string? id)
    {
        if (id == null) {
            throw new EntityValidationException("Id is null. Id is require and cannot be null");
        }
        bool isValidGuid = Guid.TryParse(id.ToString(), out var _);
        if (! isValidGuid) {
            throw new EntityValidationException("Invalid guid. The id is not in a valid Guid pattern");
        }
    }
}
