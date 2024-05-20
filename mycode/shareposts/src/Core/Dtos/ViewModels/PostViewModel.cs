using System;

namespace Shareposts.Core.Dtos.ViewModels;

public struct PostViewModel
{
    public string? id;
    public string? title;
    public string? body;
    public DateTime? createdAt;

    public string? authorId;
    public string? authorName;
}
