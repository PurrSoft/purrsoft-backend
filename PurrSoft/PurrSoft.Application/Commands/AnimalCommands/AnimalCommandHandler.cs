using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Commands.AnimalCommands;

public class AnimalCommandHandler(
    IRepository<Animal> animalRepository,
    IConfiguration configuration,
    ILogRepository<AnimalCommandHandler> logRepository)
    : IRequestHandler<AnimalCreateCommand, CommandResponse<string>>,
      IRequestHandler<AnimalUpdateCommand, CommandResponse>,
      IRequestHandler<AnimalDeleteCommand, CommandResponse>
{
    public async Task<CommandResponse<string>> Handle(AnimalCreateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Guid guid = Guid.NewGuid();
            animalRepository.Add(new Animal
            {
                Id = guid,
                AnimalType = request.AnimalType,
                Name = request.Name,
                YearOfBirth = request.YearOfBirth,
                Gender = request.Gender,
                Sterilized = request.Sterilized,
                ImageUrl = request.ImageUrl
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

    public async Task<CommandResponse> Handle(AnimalUpdateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var animal = await animalRepository
                .Query(x => x.Id == request.Id)
                .FirstOrDefaultAsync();

            animal.Name = request.Name;
            animal.AnimalType = request.AnimalType;
            animal.YearOfBirth = request.YearOfBirth;
            animal.Gender = request.Gender;
            animal.Sterilized = request.Sterilized;
            animal.ImageUrl = request.ImageUrl;
            
            await animalRepository.SaveChangesAsync(cancellationToken);

            return CommandResponse.Ok();
        }
        catch (Exception ex)
        {
            logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }

    public async Task<CommandResponse> Handle(AnimalDeleteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var animal = await animalRepository
                .Query(x => x.Id == request.Id)
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