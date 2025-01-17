using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.Queries.EventQueries;

public static class EventFilters
{
    public static IQueryable<Event> ApplyFilter(
        this IQueryable<Event> eventsQuery,
        GetFilteredEventsQueries query)
    {
        if (!string.IsNullOrEmpty(query.Search))
        {
            eventsQuery = eventsQuery
                .Where(e => 
                    e.Title.Contains(query.Search) || 
                   (!string.IsNullOrEmpty(e.Subtitle) ? e.Subtitle.Contains(query.Search) : false)
                    );
        }
        if (query.Date.HasValue)
        {
            eventsQuery = eventsQuery
                .Where(e => e.Date.HasValue ? e.Date.Value == query.Date.Value : false);
        }
        if (!string.IsNullOrEmpty(query.Location))
        {
            eventsQuery = eventsQuery
                .Where(e => e.Location == query.Location);
        }
        if (!string.IsNullOrEmpty(query.AttendingVolunteer))
        {
            eventsQuery = eventsQuery
                .Where(e => e.EventVolunteerMappings != null ? e.EventVolunteerMappings.Any(evm => evm.VolunteerId == query.AttendingVolunteer) : false);
        }

        if (query.FromDate.HasValue)
        {
            eventsQuery = eventsQuery
                .Where(e => e.Date.HasValue ? e.Date.Value >= query.FromDate.Value : false);
        }

        if (query.ToDate.HasValue)
        {
            eventsQuery = eventsQuery
                .Where(e => e.Date.HasValue ? e.Date.Value <= query.ToDate.Value : false);
        }


        return eventsQuery;
    }
}
