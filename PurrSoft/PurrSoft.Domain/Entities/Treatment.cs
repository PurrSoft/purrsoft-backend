namespace PurrSoft.Domain.Entities;

public class Treatment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid IdAnimal { get; set; }
    public string? ImageUrl { get; set; }
    public required string MedicationName { get; set; }
    public required string MedicationTime { get; set; }
    public string? ExtraInfo { get; set; }
    public required DateTime TreatmentStart { get; set; }
    public required DateTime TreatmentEnd { get; set; }
    public required int TreatmentDays { get; set; }

    public Animal Animal { get; set; } // Navigation property to the associated Animal
}