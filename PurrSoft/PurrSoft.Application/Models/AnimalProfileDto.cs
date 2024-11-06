namespace PurrSoft.Application.Models
{
    public class AnimalProfileDto
    {
        public Guid Id { get; set; } // Unique identifier for the animal profile
        public string? Passport { get; set; } // Animal's health or identification document
        public string? Microchip { get; set; } // Unique microchip identifier for the animal
        public string? CurrentDisease { get; set; } // The current medical condition of the animal
        public string? CurrentMedication { get; set; } // Medication the animal is currently taking
        public string? PastDisease { get; set; } // Past medical conditions
        public string? CurrentTreatment { get; set; } // Ongoing treatments for the animal
        public string? RabiesVaccine { get; set; } // Rabies vaccination status
        public string? MultivalentVaccine { get; set; } // Details of multivalent vaccine status
    }
}