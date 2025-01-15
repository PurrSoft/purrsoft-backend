namespace PurrSoft.Application.Models
{
    public class TreatmentDto
    {
        public Guid Id { get; set; } // Unique identifier for the treatment
        public Guid IdAnimal { get; set; } // ID of the associated animal
        public string? ImageUrl { get; set; } // URL for the medicine image
        public string MedicationName { get; set; } // Name of the medication
        public string MedicationTime { get; set; } // Time when the medication is administered
        public string? ExtraInfo { get; set; } // Additional information
        public DateTime TreatmentStart { get; set; } // Start date of the treatment
        public DateTime TreatmentEnd { get; set; } // End date of the treatment
        public int TreatmentDays { get; set; } // Total number of treatment days
    }
}