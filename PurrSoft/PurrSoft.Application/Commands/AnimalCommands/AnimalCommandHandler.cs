using MediatR;
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
    : IRequestHandler<AnimalGetCommand, CollectionResponse<Animal>>,
        IRequestHandler<AnimalCreateCommand, CommandResponse<int>>,
        IRequestHandler<AnimalUpdateCommand, CommandResponse<Animal>>,
        IRequestHandler<AnimalDeleteCommand, CommandResponse<Animal>>
{
    public Task<CollectionResponse<Animal>> Handle(AnimalGetCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var animals = animalRepository.Query().ToList();
            return null;
        }
        catch (Exception ex)
        {
            logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }

    public Task<CommandResponse<int>> Handle(AnimalCreateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return null;
        }
        catch (Exception ex)
        {
            logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }

    public Task<CommandResponse<Animal>> Handle(AnimalUpdateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return null;
        }
        catch (Exception ex)
        {
            logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }

    public Task<CommandResponse<Animal>> Handle(AnimalDeleteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return null;
        }
        catch (Exception ex)
        {
            logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }
}