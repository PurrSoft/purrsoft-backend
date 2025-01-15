using MediatR;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews;
using PurrSoft.Application.QueryOverviews.Mappers;
using PurrSoft.Common.Helpful;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Queries.EventQueries;

public class EventQueryHandler(
    IRepository<Event> eventRepository
    ) :
    IRequestHandler<GetFilteredEventsQueries, CollectionResponse<EventOverview>>,
    IRequestHandler<GetEventByIdQuery, EventDto>
{
    public async Task<CollectionResponse<EventOverview>> Handle(GetFilteredEventsQueries request, CancellationToken cancellationToken)
    {
        IQueryable<Event> query = eventRepository.Query();
        query = query.ApplyFilter(request);
        int totalNumberOfItems = await query.CountAsync(cancellationToken);
        IQueryable<EventOverview> eventOverviews = query.ProjectToOverview();
        eventOverviews = eventOverviews
            .SortAndPaginate(request.SortBy, request.SortOrder, request.Skip, request.Take);
        List<EventOverview> eventOverviewsList =
            await eventOverviews.ToListAsync(cancellationToken);

        return new CollectionResponse<EventOverview>(
            eventOverviewsList,
            totalNumberOfItems);
    }

    public async Task<EventDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var eventDto = await eventRepository
            .Query(x => x.Id.ToString() == request.Id)
            .ProjectToDto()
            .FirstOrDefaultAsync(cancellationToken);

        return eventDto;
    }
}
