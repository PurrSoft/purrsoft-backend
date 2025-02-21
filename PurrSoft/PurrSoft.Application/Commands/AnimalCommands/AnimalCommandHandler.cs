﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Application.Interfaces;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews.Mappers;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Entities.Enums;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Commands.AnimalCommands;

public class AnimalCommandHandler(
    IRepository<Animal> animalRepository,
    IConfiguration configuration,
    ISignalRService signalRService,
    ILogRepository<AnimalCommandHandler> logRepository)
    : IRequestHandler<CreateAnimalCommand, CommandResponse>,
      IRequestHandler<UpdateAnimalCommand, CommandResponse>,
      IRequestHandler<DeleteAnimalCommand, CommandResponse>
{
    public async Task<CommandResponse> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Guid guid = Guid.NewGuid();
            Animal animal = new Animal
            {
                Id = guid,
                AnimalType = request.animalDto.AnimalType != null ? Enum.Parse<AnimalType>(request.animalDto.AnimalType) : null,
                Name = request.animalDto.Name,
                YearOfBirth = request.animalDto.YearOfBirth,
                Gender = request.animalDto.Gender,
                Sterilized = request.animalDto.Sterilized,
                Passport = request.animalDto.Passport,
                ImageUrls = request.animalDto.ImageUrls ?? new List<string>()
            };
            animalRepository.Add(animal);

            await animalRepository.SaveChangesAsync(cancellationToken);

            AnimalDto? animalDto = Queryable
                .AsQueryable(new List<Animal> { animal })
                .ProjectToDto()
                .FirstOrDefault();
            await signalRService.NotifyAllAsync<Animal>(NotificationOperationType.Add, animalDto);

            return CommandResponse.Ok(guid.ToString());
        }
        catch (Exception ex)
        {
            logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }

    public async Task<CommandResponse> Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var animal = await animalRepository
                .Query(x => x.Id == Guid.Parse(request.animalDto.Id))
                .FirstOrDefaultAsync();

            animal.Name = request.animalDto.Name ?? animal.Name;
            animal.AnimalType = request.animalDto.AnimalType != null ? Enum.Parse<AnimalType>(request.animalDto.AnimalType) : animal.AnimalType;
            animal.YearOfBirth = request.animalDto.YearOfBirth ?? animal.YearOfBirth;
            animal.Gender = request.animalDto.Gender ?? animal.Gender;
            animal.Sterilized = request.animalDto.Sterilized ?? animal.Sterilized;
            animal.Passport = request.animalDto.Passport ?? animal.Passport;
            animal.ImageUrls = request.animalDto.ImageUrls ?? animal.ImageUrls;
            
            await animalRepository.SaveChangesAsync(cancellationToken);

            AnimalDto? animalDto = Queryable
                .AsQueryable(new List<Animal> { animal })
                .ProjectToDto()
                .FirstOrDefault();
            await signalRService.NotifyAllAsync<Animal>(NotificationOperationType.Update, animalDto);

            return CommandResponse.Ok();
        }
        catch (Exception ex)
        {
            logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }

    public async Task<CommandResponse> Handle(DeleteAnimalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var animal = await animalRepository
                .Query(x => x.Id == Guid.Parse(request.Id))
                .FirstOrDefaultAsync();

            if (animal == null)
            {
                return CommandResponse.Failed("Animal not found.");
            }

            animalRepository.Remove(animal);

            await animalRepository.SaveChangesAsync(cancellationToken);

            await signalRService.NotifyAllAsync<Animal>(NotificationOperationType.Delete, request.Id);

            return CommandResponse.Ok();
        }
        catch (Exception ex)
        {
            logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }
}