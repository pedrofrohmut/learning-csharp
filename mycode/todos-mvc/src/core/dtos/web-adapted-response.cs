namespace TodosMvc.Core.Dtos;

public struct WebAdaptedResponse
{
    public int statusCode;
    public string? message;
    public dynamic? body;
}
