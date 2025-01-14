using PurrSoft.Application.Models;
using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.QueryOverviews.Mappers;

public static class AnimalProfileMapper
{
    public static IQueryable<AnimalProfileDto> ProjectToDto(this IQueryable<AnimalProfile> query)
    {
        return query.Select(ap => new AnimalProfileDto()
        {
            AnimalId = ap.AnimalId,
            Contract = ap.Contract,
            CurrentDisease = ap.CurrentDisease,
            CurrentMedication = ap.CurrentMedication,
            PastDisease = ap.PastDisease,
            Microchip = ap.Microchip,
            ExternalDeworming = ap.ExternalDeworming,
            InternalDeworming = ap.InternalDeworming,
            CurrentTreatment = ap.CurrentTreatment,
            MultivalentVaccine = ap.MultivalentVaccine,
            RabiesVaccine = ap.RabiesVaccine,
            FIVFeLVTest = ap.FIVFeLVTest,
            CoronavirusVaccine = ap.CoronavirusVaccine,
            GiardiaTest = ap.GiardiaTest,
            EarMiteTreatment = ap.EarMiteTreatment,
            IntakeNotes = ap.IntakeNotes,
            AdditionalMedicalInfo = ap.AdditionalMedicalInfo,
            AdditionalInfo = ap.AdditionalInfo,
            MedicalAppointments = ap.MedicalAppointments,
            RefillReminders = ap.RefillReminders,
            UsefulLinks = ap.UsefulLinks
        });
    }
}
