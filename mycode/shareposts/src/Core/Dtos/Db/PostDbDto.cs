using System;

namespace Shareposts.Core.Dtos.Db;

public struct PostDbDto
{
    public string? id;
    public string? title;
    public string? body;
    public string? userId;
    public DateTime? createdAt;
}
