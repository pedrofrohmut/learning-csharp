using System;

namespace Shareposts.Core.Dtos.Db;

public struct UserDbDto
{
    public string? id;
    public string? name;
    public string? email;
    public string? phone;
    public string? passwordHash;
    public DateTime createdAt;
}
