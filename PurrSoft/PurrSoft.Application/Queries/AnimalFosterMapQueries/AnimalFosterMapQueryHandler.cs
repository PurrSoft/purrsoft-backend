using MediatR;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews;
using PurrSoft.Application.QueryOverviews.Mappers;
using PurrSoft.Common.Helpful;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Queries.AnimalFosterMapQueries;

public class AnimalFosterMapQueryHandler(IRepository<AnimalFosterMap> animalFosterMapRepository) :
	IRequestHandler<GetAnimalFosterMapById, AnimalFosterMapDto?>,
	IRequestHandler<GetFilteredAnimalFosterMapsQueries, CollectionResponse<AnimalFosterMapOverview>>
{
	public async Task<AnimalFosterMapDto?> Handle(GetAnimalFosterMapById request, CancellationToken cancellationToken)
	{
		AnimalFosterMapDto? animalFosterMapDto = await animalFosterMapRepository.Query(f => f.Id.ToString() == request.Id)
			.ProjectToDto()
			.FirstOrDefaultAsync(cancellationToken);

		return animalFosterMapDto;
	}

	public async Task<CollectionResponse<AnimalFosterMapOverview>> Handle(
		GetFilteredAnimalFosterMapsQueries request, CancellationToken cancellationToken
		)
	{
		IQueryable<AnimalFosterMap> query = animalFosterMapRepository.Query();

		request.StartFosteringDate = request.StartFosteringDate != null ?
									DateTime.SpecifyKind(request.StartFosteringDate.Value, DateTimeKind.Utc)
									: null;
		request.EndFosteringDate = request.EndFosteringDate != null ?
									DateTime.SpecifyKind(request.EndFosteringDate.Value, DateTimeKind.Utc)
									: null;
		query = query.ApplyFilter(request);
		IQueryable<AnimalFosterMapOverview> overview = query.ProjectToOverview();
		overview = overview
			.SortAndPaginate(request.SortBy, request.SortOrder, request.Skip, request.Take);
		List<AnimalFosterMapOverview> animalFosterMapOverviewsList = await overview.ToListAsync(cancellationToken);
		return new CollectionResponse<AnimalFosterMapOverview>(animalFosterMapOverviewsList, animalFosterMapOverviewsList.Count);
	}
}