using System.ComponentModel.DataAnnotations.Schema;

namespace PurrSoft.Domain.Entities;

public class AnimalProfile
{
    public Guid Id { get; set; } // Unique identifier for the animal profile
    public string? CurrentDisease { get; set; } // The current medical condition of the animal
    public string? CurrentMedication { get; set; } // Medication the animal is currently taking
    public string? PastDisease { get; set; } // Past medical conditions

    public Animal? Animal { get; set; } // Associated animal entity
    public Guid AnimalId { get; set; } // Foreign key referencing the associated animal

    
    public string?  Passport { get; set; } // Animal's health or identification document
    public string? Microchip { get; set; } // Unique microchip identifier for the animal
    public string? ExternalDeworming { get; set; } // Records of external parasite treatments
    public string? InternalDeworming { get; set; } // Records of internal parasite treatments
    public string? CurrentTreatment { get; set; } // Ongoing treatments for the animal
    public string? MultivalentVaccine { get; set; } // Details of multivalent vaccine status
    public string? RabiesVaccine { get; set; } // Rabies vaccination status
    public string? FIVFeLVTest { get; set; } // FIV/FeLV test results (Feline diseases)
    public string? CoronavirusVaccine { get; set; } // Status of coronavirus vaccination
    public string? GiardiaTest { get; set; } // Results of Giardia infection test
    public string? EarMiteTreatment { get; set; } // Ear mite (Auricular mange) treatment details
    public string? IntakeNotes { get; set; } // Notes recorded when the animal was taken in
    public string? AdditionalMedicalInfo { get; set; } // Other medical information not covered elsewhere
    public string? AdditionalInfo { get; set; } // General additional notes
    public string? MedicalAppointments { get; set; } // Upcoming or past medical appointments
    public string? RefillReminders { get; set; } // Medication refill reminders or notes
    [Column(TypeName = "jsonb")] 
    public List<string>? UsefulLinks { get; set; } = new List<string>(); // List of helpful links for the profile
}
