using Newtonsoft.Json;
using PurrSoft.Domain.Entities.JsonConvertor;

namespace PurrSoft.Application.Models
{
    public class AnimalProfileDto
    {
        public Guid AnimalId { get; set; }
        public string? CurrentDisease { get; set; }
        public string? CurrentMedication { get; set; }
        public string? PastDisease { get; set; }
        public string? Passport { get; set; }
        public string? Microchip { get; set; }
        public string? ExternalDeworming { get; set; }
        public string? InternalDeworming { get; set; }
        public string? CurrentTreatment { get; set; }
        public string? MultivalentVaccine { get; set; }
        public string? RabiesVaccine { get; set; }
        public string? FIVFeLVTest { get; set; }
        public string? CoronavirusVaccine { get; set; }
        public string? GiardiaTest { get; set; }
        public string? EarMiteTreatment { get; set; }
        public string? IntakeNotes { get; set; }
        public string? AdditionalMedicalInfo { get; set; }
        public string? AdditionalInfo { get; set; }
        public string? MedicalAppointments { get; set; }
        public string? RefillReminders { get; set; }
        
        [JsonProperty("UsefulLinks")]
        [JsonConverter(typeof(SingleOrArrayJsonConverter<string>))]
        public List<string>? UsefulLinks { get; set; } = new();
    }
}