using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Commands.AnimalCommands;
using PurrSoft.Application.Common;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Commands.EventCommands;

public class EventCommandHandler(
    IRepository<Event> eventRepository,
    IRepository<EventVolunteerMap> eventVolunteerMapRepository,
    IConfiguration configuration,
    ILogRepository<AnimalCommandHandler> logRepository)
    : IRequestHandler<CreateEventCommand, CommandResponse>,
      IRequestHandler<UpdateEventCommand, CommandResponse>,
      IRequestHandler<DeleteEventCommand, CommandResponse>
{
    public async Task<CommandResponse> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Guid guid = Guid.NewGuid();
            eventRepository.Add(new Event
            {
                Id = guid,
                Title = request.EventDto.Title,
                Subtitle = request.EventDto.Subtitle,
                Date = request.EventDto.Date,
                Location = request.EventDto.Location,
                Description = request.EventDto.Description
            });

            if (request.EventDto.Volunteers != null)
            {
                eventVolunteerMapRepository.AddRangeAsync(request.EventDto.Volunteers
                    .Select(x => new EventVolunteerMap
                    {
                        EventId = guid,
                        VolunteerId = x
                    })
                    .ToList());
            }

            await eventRepository.SaveChangesAsync(cancellationToken);
            await eventVolunteerMapRepository.SaveChangesAsync(cancellationToken);

            return CommandResponse.Ok(guid.ToString());
        }
        catch (Exception ex)
        {
            logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }

    public async Task<CommandResponse> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var eventEntity = await eventRepository
                .Query(x => x.Id == request.EventDto.Id)
                .FirstOrDefaultAsync();

            if (eventEntity == null)
            {
                return CommandResponse.Failed("Event not found.");
            }

            eventEntity.Title = request.EventDto.Title ?? eventEntity.Title;
            eventEntity.Subtitle = request.EventDto.Subtitle ?? eventEntity.Subtitle;
            eventEntity.Date = request.EventDto.Date ?? eventEntity.Date;
            eventEntity.Location = request.EventDto.Location ?? eventEntity.Location;
            eventEntity.Description = request.EventDto.Description ?? eventEntity.Description;

            await eventRepository.SaveChangesAsync(cancellationToken);

            if (request.EventDto.Volunteers != null && request.EventDto.Volunteers.Any())
            {
                if (eventEntity.EventVolunteerMappings != null && eventEntity.EventVolunteerMappings.Any())
                {
                    eventVolunteerMapRepository.RemoveRange(eventEntity.EventVolunteerMappings);
                }
                eventVolunteerMapRepository.AddRangeAsync(request.EventDto.Volunteers
                    .Select(x => new EventVolunteerMap
                    {
                        VolunteerId = x,
                        EventId = eventEntity.Id
                    }
                    ).ToList());
            }

            return CommandResponse.Ok();
        }
        catch (Exception ex)
        {
            logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }

    public async Task<CommandResponse> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var eventEntity = await eventRepository
                .Query(x => x.Id == Guid.Parse(request.Id))
                .FirstOrDefaultAsync();

            if (eventEntity == null)
            {
                return CommandResponse.Failed("Event not found.");
            }

            eventRepository.Remove(eventEntity);

            await eventRepository.SaveChangesAsync(cancellationToken);

            return CommandResponse.Ok();
        }
        catch (Exception ex)
        {
            logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }
}
