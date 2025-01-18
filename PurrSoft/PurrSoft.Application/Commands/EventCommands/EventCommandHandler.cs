using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Commands.AnimalCommands;
using PurrSoft.Application.Common;
using PurrSoft.Application.Interfaces;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews;
using PurrSoft.Application.QueryOverviews.Mappers;
using PurrSoft.Common.Identity;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Entities.Enums;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Commands.EventCommands;

public class EventCommandHandler(
    IRepository<Event> eventRepository,
    IRepository<EventVolunteerMap> eventVolunteerMapRepository,
    IConfiguration configuration,
    ILogRepository<AnimalCommandHandler> logRepository,
    ISignalRService signalRService,
    IRepository<Notifications> notificationsRepository,
    ICurrentUserService currentUserService)
    : IRequestHandler<CreateEventCommand, CommandResponse>,
      IRequestHandler<UpdateEventCommand, CommandResponse>,
      IRequestHandler<DeleteEventCommand, CommandResponse>
{
    public async Task<CommandResponse> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Guid guid = Guid.NewGuid();
            Event eventObj = new Event
            {
                Id = guid,
                Title = request.EventDto.Title,
                Subtitle = request.EventDto.Subtitle,
                Date = request.EventDto.Date.HasValue? DateTime.SpecifyKind(request.EventDto.Date.Value, DateTimeKind.Utc) : null,
                Location = request.EventDto.Location,
                Description = request.EventDto.Description
            };
            eventRepository.Add(eventObj);

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

            EventOverview? eventOverview = await eventRepository
                .Query(x => x.Id == guid)
                .Include(x => x.EventVolunteerMappings)
                .ThenInclude(x => x.Volunteer)
                .ThenInclude(x => x.User)
                .ProjectToOverview()
                .FirstOrDefaultAsync(cancellationToken);
            await signalRService.NotifyAllAsync<Event>(NotificationOperationType.Add, eventOverview);

            CurrentUser currentUser = await currentUserService.GetCurrentUser();

            Notifications notifications = new Notifications
            {
                Id = Guid.NewGuid(),
                UserId = currentUser.UserId,
                Type = "New Event",
                Message = $"New event {eventObj.Title} has been created.",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            notificationsRepository.Add(notifications);
            NotificationsDto notificationsDto = new NotificationsDto
            {
                Id = notifications.Id,
                Type = notifications.Type,
                Message = notifications.Message,
                IsRead = notifications.IsRead
            };

            await signalRService.NotifyAllAsync<Notifications>(NotificationOperationType.Add, notificationsDto);

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
            eventEntity.Date = request.EventDto.Date.HasValue ? DateTime.SpecifyKind(request.EventDto.Date.Value, DateTimeKind.Utc) : eventEntity.Date;
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

            EventOverview? eventOverview = Queryable
                .AsQueryable(new List<Event> { eventEntity })
                .ProjectToOverview()
                .FirstOrDefault();
            await signalRService.NotifyAllAsync<Event>(NotificationOperationType.Update, eventOverview);

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

            await signalRService.NotifyAllAsync<Event>(NotificationOperationType.Delete, request.Id);

            return CommandResponse.Ok();
        }
        catch (Exception ex)
        {
            logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }
}
