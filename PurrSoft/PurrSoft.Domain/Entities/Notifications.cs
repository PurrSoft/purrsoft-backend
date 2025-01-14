namespace PurrSoft.Domain.Entities;


public class Notifications
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } 
    public required string Type { get; set; }
    public required string Message { get; set; }
    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property to ApplicationUser
    public ApplicationUser User { get; set; }
}
