﻿using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Commands.AnimalProfileCommands
{
    public class AnimalProfileCommandHandler(
        IRepository<AnimalProfile> animalProfileRepository,
        IRepository<Animal> animalRepository,
        ILogRepository<AnimalProfileCommandHandler> logRepository)
        : IRequestHandler<AnimalProfileCommands.AnimalProfileCreateCommand, CommandResponse<Guid>>,
          IRequestHandler<AnimalProfileCommands.AnimalProfileUpdateCommand, CommandResponse>,
          IRequestHandler<AnimalProfileCommands.AnimalProfileDeleteCommand, CommandResponse>
    {
        public async Task<CommandResponse<Guid>> Handle(AnimalProfileCommands.AnimalProfileCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Ensure that the associated Animal exists
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
                    Id = Guid.NewGuid(),
                    AnimalId = request.AnimalId,
                    CurrentDisease = request.CurrentDisease,
                    CurrentMedication = request.CurrentMedication,
                    PastDisease = request.PastDisease
                };

                animalProfileRepository.Add(newProfile);
                await animalProfileRepository.SaveChangesAsync(cancellationToken);

                return CommandResponse.Ok(newProfile.Id);
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
                    .Query(x => x.Id == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                if (profile == null)
                {
                    return CommandResponse.Failed(new List<ValidationFailure>
                    {
                        new("Id", "Animal profile not found.")
                    });
                }

                profile.CurrentDisease = request.CurrentDisease;
                profile.CurrentMedication = request.CurrentMedication;
                profile.PastDisease = request.PastDisease;

                await animalProfileRepository.SaveChangesAsync(cancellationToken);

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
                    .Query(x => x.Id == request.Id)
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
}