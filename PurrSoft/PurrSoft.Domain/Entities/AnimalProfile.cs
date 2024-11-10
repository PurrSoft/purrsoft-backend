namespace PurrSoft.Domain.Entities;

public class AnimalProfile
{
    public Guid Id { get; set; }
    public string? CurrentDisease { get; set; }
    public string? CurrentMedication { get; set; }
    public string? PastDisease { get; set; }
    
    public Animal? Animal { get; set; }
    
    public Guid AnimalId { get; set; }
    
}