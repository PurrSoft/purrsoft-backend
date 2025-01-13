using MediatR;
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
            animalRepository.Add(new Animal
            {
                Id = guid,
                AnimalType = Enum.Parse<AnimalType>(request.animalDto.AnimalType),
                Name = request.animalDto.Name,
                YearOfBirth = request.animalDto.YearOfBirth,
                Gender = request.animalDto.Gender,
                Sterilized = request.animalDto.Sterilized,
                ImageUrl = request.animalDto.ImageUrl
            });

            await animalRepository.SaveChangesAsync(cancellationToken);

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

            animal.Name = request.animalDto.Name;
            animal.AnimalType = Enum.Parse<AnimalType>(request.animalDto.AnimalType);
            animal.YearOfBirth = request.animalDto.YearOfBirth;
            animal.Gender = request.animalDto.Gender;
            animal.Sterilized = request.animalDto.Sterilized;
            animal.ImageUrl = request.animalDto.ImageUrl;
            
            await animalRepository.SaveChangesAsync(cancellationToken);

            var animalDto = Queryable.AsQueryable(new List<Animal> { animal }).ProjectToDto().FirstOrDefault();

            await signalRService.NotifyAllAsync<AnimalDto>(OperationType.Update, animalDto);

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

            return CommandResponse.Ok();
        }
        catch (Exception ex)
        {
            logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }
}