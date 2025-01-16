using PurrSoft.Application.Common;

namespace PurrSoft.Application.Commands.AnimalProfileCommands;

public class AnimalProfileCommands
{
    public class AnimalProfileCreateCommand : BaseRequest<CommandResponse<Guid>>
    {
        public Guid AnimalId { get; set; }
        public string? Contract { get; set; }
        public string? ContractState { get; set; }
        public DateTime? ShelterCheckIn { get; set; }
        public string? Microchip { get; set; }
        public string? CurrentDisease { get; set; }
        public string? CurrentMedication { get; set; }
        public string? PastDisease { get; set; }
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
        public List<string>? UsefulLinks { get; set; } = new List<string>();
    }

    public class AnimalProfileUpdateCommand : BaseRequest<CommandResponse>
    {
        
        public Guid AnimalId { get; set; }
        public string? Contract { get; set; }
        public string? ContractState { get; set; }
        public DateTime? ShelterCheckIn { get; set; }
        public string? Microchip { get; set; }
        public string? CurrentDisease { get; set; }
        public string? CurrentMedication { get; set; }
        public string? PastDisease { get; set; }
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
        public List<string>? UsefulLinks { get; set; } = new List<string>();
    }

    public class AnimalProfileDeleteCommand : BaseRequest<CommandResponse>
    {
        public Guid Id { get; set; }
    }
}
