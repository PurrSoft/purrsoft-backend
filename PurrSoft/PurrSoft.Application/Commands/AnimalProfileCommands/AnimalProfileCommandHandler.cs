﻿using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Application.Interfaces;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews.Mappers;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Entities.Enums;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Commands.AnimalProfileCommands;

public class AnimalProfileCommandHandler(
    IRepository<AnimalProfile> animalProfileRepository,
    IRepository<Animal> animalRepository,
    ILogRepository<AnimalProfileCommandHandler> logRepository,
    ISignalRService signalRService)
    : IRequestHandler<AnimalProfileCommands.AnimalProfileCreateCommand, CommandResponse<Guid>>,
      IRequestHandler<AnimalProfileCommands.AnimalProfileUpdateCommand, CommandResponse>,
      IRequestHandler<AnimalProfileCommands.AnimalProfileDeleteCommand, CommandResponse>
{
    public async Task<CommandResponse<Guid>> Handle(AnimalProfileCommands.AnimalProfileCreateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var animal = await animalRepository.Query(x => x.Id == request.AnimalId).FirstOrDefaultAsync(cancellationToken);
            if (animal == null)
            {
                return CommandResponse.Failed(new List<ValidationFailure>
                {
                    new("AnimalId", "Animal not found.")
                }) as CommandResponse<Guid> ?? throw new InvalidOperationException();
            }

            var newProfile = new AnimalProfile
            {
                AnimalId = request.AnimalId,
                Contract = request.Contract,
                ContractState = request.ContractState != null ? Enum.Parse<ContractState>(request.ContractState) : null,
                ShelterCheckIn = request.ShelterCheckIn,
                Microchip = request.Microchip,
                CurrentDisease = request.CurrentDisease,
                CurrentMedication = request.CurrentMedication,
                PastDisease = request.PastDisease,
                ExternalDeworming = request.ExternalDeworming,
                InternalDeworming = request.InternalDeworming,
                CurrentTreatment = request.CurrentTreatment,
                MultivalentVaccine = request.MultivalentVaccine,
                RabiesVaccine = request.RabiesVaccine,
                FIVFeLVTest = request.FIVFeLVTest,
                CoronavirusVaccine = request.CoronavirusVaccine,
                GiardiaTest = request.GiardiaTest,
                EarMiteTreatment = request.EarMiteTreatment,
                IntakeNotes = request.IntakeNotes,
                AdditionalMedicalInfo = request.AdditionalMedicalInfo,
                AdditionalInfo = request.AdditionalInfo,
                MedicalAppointments = request.MedicalAppointments,
                RefillReminders = request.RefillReminders,
                UsefulLinks = request.UsefulLinks
            };

            animalProfileRepository.Add(newProfile);
            await animalProfileRepository.SaveChangesAsync(cancellationToken);

            AnimalProfileDto? profileDto = Queryable
                .AsQueryable(new List<AnimalProfile> { newProfile })
                .ProjectToDto()
                .FirstOrDefault();
            await signalRService.NotifyAllAsync<AnimalProfile>(NotificationOperationType.Add, profileDto);

                return CommandResponse.Ok(newProfile.AnimalId);
            }
            catch (Exception ex)
            {
                logRepository.LogException(LogLevel.Error, ex);
                throw;
            }
        }

    public async Task<CommandResponse> Handle(AnimalProfileCommands.AnimalProfileUpdateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var profile = await animalProfileRepository
                .Query(x => x.AnimalId == request.AnimalId)
                .FirstOrDefaultAsync(cancellationToken);

            if (profile == null)
            {
                return CommandResponse.Failed(new List<ValidationFailure>
                {
                    new("Id", "Animal profile not found.")
                });
            }

            profile.Contract = request.Contract ?? profile.Contract;
            profile.ContractState = request.ContractState != null ? Enum.Parse<ContractState>(request.ContractState) : profile.ContractState;
            profile.ShelterCheckIn = request.ShelterCheckIn ?? profile.ShelterCheckIn;
            profile.Microchip = request.Microchip ?? profile.Microchip;
            profile.CurrentDisease = request.CurrentDisease ?? profile.CurrentDisease;
            profile.CurrentMedication = request.CurrentMedication ?? profile.CurrentMedication;
            profile.PastDisease = request.PastDisease ?? profile.PastDisease;
            profile.ExternalDeworming = request.ExternalDeworming ?? profile.ExternalDeworming;
            profile.InternalDeworming = request.InternalDeworming ?? profile.InternalDeworming;
            profile.CurrentTreatment = request.CurrentTreatment ?? profile.CurrentTreatment;
            profile.MultivalentVaccine = request.MultivalentVaccine ?? profile.MultivalentVaccine;
            profile.RabiesVaccine = request.RabiesVaccine ?? profile.RabiesVaccine;
            profile.FIVFeLVTest = request.FIVFeLVTest ?? profile.FIVFeLVTest;
            profile.CoronavirusVaccine = request.CoronavirusVaccine ?? profile.CoronavirusVaccine;
            profile.GiardiaTest = request.GiardiaTest ?? profile.GiardiaTest;
            profile.EarMiteTreatment = request.EarMiteTreatment ?? profile.EarMiteTreatment;
            profile.IntakeNotes = request.IntakeNotes ?? profile.IntakeNotes;
            profile.AdditionalMedicalInfo = request.AdditionalMedicalInfo ?? profile.AdditionalMedicalInfo;
            profile.AdditionalInfo = request.AdditionalInfo ?? profile.AdditionalInfo;
            profile.MedicalAppointments = request.MedicalAppointments ?? profile.MedicalAppointments;
            profile.RefillReminders = request.RefillReminders ?? profile.RefillReminders;
            profile.UsefulLinks = request.UsefulLinks;

            await animalProfileRepository.SaveChangesAsync(cancellationToken);

            AnimalProfileDto? profileDto = Queryable
                .AsQueryable(new List<AnimalProfile> { profile })
                .ProjectToDto()
                .FirstOrDefault();
            await signalRService.NotifyAllAsync<AnimalProfile>(NotificationOperationType.Update, profileDto);

                return CommandResponse.Ok();
            }
            catch (Exception ex)
            {
                logRepository.LogException(LogLevel.Error, ex);
                return CommandResponse.Failed(new List<ValidationFailure>
                {
                    new("AnimalProfile", "Failed to update animal profile.")
                });
            }
        }

    public async Task<CommandResponse> Handle(AnimalProfileCommands.AnimalProfileDeleteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var profile = await animalProfileRepository
                .Query(x => x.AnimalId == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (profile == null)
            {
                return CommandResponse.Failed(new List<ValidationFailure>
                {
                    new("Id", "Animal profile not found.")
                });
            }

            animalProfileRepository.Remove(profile);
            await animalProfileRepository.SaveChangesAsync(cancellationToken);

            await signalRService.NotifyAllAsync<AnimalProfile>(NotificationOperationType.Delete, request.Id);

            return CommandResponse.Ok();
        }
        catch (Exception ex)
        {
            logRepository.LogException(LogLevel.Error, ex);
            return CommandResponse.Failed(new List<ValidationFailure>
            {
                new("AnimalProfile", "Failed to delete animal profile.")
            });
        }
    }
}
