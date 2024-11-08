using MediatR;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews;
using PurrSoft.Application.QueryOverviews.Mappers;
using PurrSoft.Common.Helpful;
using PurrSoft.Common.Identity;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Queries.FosterQueries;

public class FosterQueryHandler
	(IRepository<Foster> _fosterRepository,
	IRepository<ApplicationUser> _userRepository,
	ICurrentUserService _currentUserService)
	: IRequestHandler<GetFilteredFostersQueries, CollectionResponse<FosterOverview>>,
	IRequestHandler<GetFosterByIdQuery, FosterDto>
{
	public async Task<CollectionResponse<FosterOverview>> Handle(
		GetFilteredFostersQueries request, CancellationToken cancellationToken
		)
	{
		IQueryable<Foster> query = _fosterRepository.Query();
		query = query.ApplyFilter(request);
		IQueryable<FosterOverview> overview = query.ProjectToOverview();
		overview = overview
			.SortAndPaginate(request.SortBy, request.SortOrder, request.Skip, request.Take); //request.Take defaults to 0 if not provided
		List<FosterOverview> fosterOverviewsList = await overview.ToListAsync(cancellationToken);
		return new CollectionResponse<FosterOverview>(fosterOverviewsList, fosterOverviewsList.Count);
	}

	public async Task<FosterDto> Handle(GetFosterByIdQuery request, CancellationToken cancellationToken)
	{
		CurrentUser currentUser = await _currentUserService.GetCurrentUser();

		if (currentUser == null)
		{
			throw new UnauthorizedAccessException();
		}

		ApplicationUser? applicationUser = await _userRepository.Query(u => u.Id == currentUser.UserId)
			.Include(u => u.UserRoles)
			.ThenInclude(ur => ur.Role)
			.FirstOrDefaultAsync(cancellationToken);

		var userRoles = applicationUser?.UserRoles.Select(ur => ur.Role.Name).ToList();

		// check if user is Manager or if user is Foster, then check if the Foster is the same as the one being queried
		if (userRoles != null && userRoles.Contains("Foster") && currentUser.UserId != request.Id)
		{
			throw new UnauthorizedAccessException();
		}

		FosterDto? fosterDto = await _fosterRepository.Query(f => f.UserId == request.Id)
			.ProjectToDto()
			.FirstOrDefaultAsync(cancellationToken);

		return fosterDto;
	}
}
