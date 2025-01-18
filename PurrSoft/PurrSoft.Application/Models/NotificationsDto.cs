namespace PurrSoft.Application.Models;

public class NotificationsDto
{
    public Guid Id { get; set; }
    public string? UserId { get; set; }
    public required string Type { get; set; }
    public required string Message { get; set; }
    public bool IsRead { get; set; }
}
