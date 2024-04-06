namespace TodosMvc.Core.Dtos;

public struct UserDbDto
{
    public Guid? id;
    public string? name;
    public string? email;
    public string? phone;
    public string? passwordHash;
    public string? createdAt;
}
