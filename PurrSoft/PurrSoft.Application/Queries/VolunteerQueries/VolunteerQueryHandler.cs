using AlbumStore.Application.Filtering;
using AlbumStore.Application.QueryOverviews;
using AlbumStore.Application.QueryOverviews.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Application.Common;
using PurrSoft.Common.Helpful;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace AlbumStore.Application.Queries.VolunteerQueries;

public class VolunteerQueryHandler(
    IRepository<Volunteer> _volunteerRepository) :
    IRequestHandler<GetFilteredVolunteersQueries, CollectionResponse<VolunteerOverview>>,
    IRequestHandler<GetVolunteerQuery, VolunteerOverview>
{
    public async Task<CollectionResponse<VolunteerOverview>> Handle(GetFilteredVolunteersQueries request, CancellationToken cancellationToken)
    {
        IQueryable<Volunteer> query = _volunteerRepository.Query();
        query = query.ApplyFilter(request);
        int totalNumberOfItems = await query.CountAsync(cancellationToken);
        IQueryable<VolunteerOverview> volunteerOverviews = query.ProjectToOverview();
        volunteerOverviews = volunteerOverviews.SortAndPaginate(request.SortBy, request.SortOrder, request.Skip, request.Take);
        List<VolunteerOverview> volunteerOverviewsList = await volunteerOverviews.ToListAsync(cancellationToken);

        return new CollectionResponse<VolunteerOverview>(volunteerOverviewsList, totalNumberOfItems);
    }

    public async Task<VolunteerOverview> Handle(GetVolunteerQuery request, CancellationToken cancellationToken)
    {
        VolunteerOverview? volunteerOverview = await _volunteerRepository.Query(v => v.UserId == request.Id).ProjectToOverview().FirstOrDefaultAsync(cancellationToken);

        return volunteerOverview;
    }
}

