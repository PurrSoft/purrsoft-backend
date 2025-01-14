using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Entities.Enums;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Commands.AnimalCommands;

public class AnimalCommandHandler(
    IRepository<Animal> animalRepository,
    IConfiguration configuration,
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
                AnimalType = request.animalDto.AnimalType != null ? Enum.Parse<AnimalType>(request.animalDto.AnimalType) : null,
                Name = request.animalDto.Name,
                YearOfBirth = request.animalDto.YearOfBirth,
                Gender = request.animalDto.Gender,
                Sterilized = request.animalDto.Sterilized,
                ImageUrls = request.animalDto.ImageUrls ?? new List<string>()
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

            animal.Name = request.animalDto.Name ?? animal.Name;
            animal.AnimalType = request.animalDto.AnimalType != null ? Enum.Parse<AnimalType>(request.animalDto.AnimalType) : animal.AnimalType;
            animal.YearOfBirth = request.animalDto.YearOfBirth ?? animal.YearOfBirth;
            animal.Gender = request.animalDto.Gender ?? animal.Gender;
            animal.Sterilized = request.animalDto.Sterilized ?? animal.Sterilized;
            animal.ImageUrls = request.animalDto.ImageUrls ?? animal.ImageUrls;
            
            await animalRepository.SaveChangesAsync(cancellationToken);

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