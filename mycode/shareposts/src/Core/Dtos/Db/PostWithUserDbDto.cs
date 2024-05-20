using System;

namespace Shareposts.Core.Dtos.Db;

public struct PostWithUserDbDto
{
    public string? id;
    public string? title;
    public string? body;
    public DateTime? createdAt;
    public string? authorId;
    public string? authorName;
}
