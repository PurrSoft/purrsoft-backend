using AlbumStore.Application.Filtering;
using AlbumStore.Application.QueryOverviews;
using AlbumStore.Application.QueryOverviews.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Application.Common;
using PurrSoft.Common.Helpful;
using PurrSoft.Common.Identity;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace AlbumStore.Application.Queries.VolunteerQueries;

public class VolunteerQueryHandler(
    IRepository<Volunteer> _volunteerRepository,
    IRepository<ApplicationUser> _userRepository,
    ICurrentUserService _currentUserService) :
    IRequestHandler<GetFilteredVolunteersQueries, CollectionResponse<VolunteerOverview>>,
    IRequestHandler<GetVolunteerQuery, VolunteerOverview>
{
    public async Task<CollectionResponse<VolunteerOverview>> Handle(
        GetFilteredVolunteersQueries request, 
        CancellationToken cancellationToken)
    {
        IQueryable<Volunteer> query = _volunteerRepository.Query();
        query = query.ApplyFilter(request);
        int totalNumberOfItems = await query.CountAsync(cancellationToken);
        IQueryable<VolunteerOverview> volunteerOverviews = query.ProjectToOverview();
        volunteerOverviews = volunteerOverviews
            .SortAndPaginate(request.SortBy, request.SortOrder, request.Skip, request.Take);
        List<VolunteerOverview> volunteerOverviewsList = await volunteerOverviews.ToListAsync(cancellationToken);

        return new CollectionResponse<VolunteerOverview>(volunteerOverviewsList, totalNumberOfItems);
    }

    public async Task<VolunteerOverview> Handle(GetVolunteerQuery request, CancellationToken cancellationToken)
    {
        CurrentUser currentUser = await _currentUserService.GetCurrentUser();

        if (currentUser == null)
        {
            throw new UnauthorizedAccessException();
        }

        ApplicationUser user = await _userRepository
            .Query(u => u.Id == currentUser.UserId)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(cancellationToken);

        List<string> userRoles = user.UserRoles
            .Select(ur => ur.Role.Name).ToList();

        if (userRoles != null && 
            userRoles.Contains("Volunteer") &&
            currentUser.UserId != request.Id)
        {
            throw new UnauthorizedAccessException();
        }

        VolunteerOverview? volunteerOverview = await _volunteerRepository
            .Query(v => v.UserId == request.Id).ProjectToOverview()
            .FirstOrDefaultAsync(cancellationToken);

        return volunteerOverview;
    }
}

