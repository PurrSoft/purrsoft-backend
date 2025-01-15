using PurrSoft.Application.Models;
using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.QueryOverviews.Mappers;

public static class EventMapper
{
    public static IQueryable<EventDto> ProjectToDto(this IQueryable<Event> query)
    {
        return query.Select(x => new EventDto
        {
            Id = x.Id,
            Title = x.Title,
            Subtitle = x.Subtitle,
            Date = x.Date,
            Location = x.Location,
            Description = x.Description,
            Volunteers = x.EventVolunteerMappings.Select(evm => evm.VolunteerId).ToList(),
        });
    }
    public static IQueryable<EventOverview> ProjectToOverview(this IQueryable<Event> query)
    {
        return query.Select(x => new EventOverview
        {
            Id = x.Id.ToString(),
            Title = x.Title,
            Subtitle = x.Subtitle,
            Date = x.Date.ToString(),
            Location = x.Location,
            Description = x.Description,
            AttendingVolunteers = x.EventVolunteerMappings.Select(x => x.Volunteer.User.FullName).ToList()
        });
    }
}
