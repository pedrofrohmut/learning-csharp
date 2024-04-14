namespace TodosMvc.Core.Dtos;

public struct GoalDbDto
{
    public Guid? id;
    public string? text;
    public Guid? userId;
    public DateTime? createdAt;
}
