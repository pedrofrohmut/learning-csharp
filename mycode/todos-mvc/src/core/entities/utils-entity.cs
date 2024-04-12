using TodosMvc.Core.Exceptions;

namespace TodosMvc.Core.Entities;

public static class UtilsEntity
{
    public static void ValidateId(string? id)
    {
        if (id == null) {
            throw new EntityValidationException("Id is null. Id is required and cannot be null.");
        }
        if (! Guid.TryParse(id.ToString(), out var _)) {
            throw new EntityValidationException("Id is not valid Guid format.");
        }
    }

    public static void ValidateId(Guid? id)
    {
        ValidateId(id.ToString());
    }

    public static Guid CastValidateId(string? id)
    {
        ValidateId(id);
        return Guid.Parse(id!);
    }
}
