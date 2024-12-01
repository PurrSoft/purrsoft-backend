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

public class AnimalFosterMapQueryHandler(IAnimalFosterMapRepository animalFosterMapRepository) :
	IRequestHandler<GetAnimalFosterMapsByFosterId, CollectionResponse<AnimalFosterMapDto>>,
	IRequestHandler<GetAnimalFosterMapsByAnimalId, CollectionResponse<AnimalFosterMapDto>>,
	IRequestHandler<GetAnimalFosterMapById, AnimalFosterMapDto?>,
	IRequestHandler<GetFilteredAnimalFosterMapsQueries, CollectionResponse<AnimalFosterMapOverview>>
{
	public async Task<CollectionResponse<AnimalFosterMapDto>> Handle(GetAnimalFosterMapsByFosterId request, CancellationToken cancellationToken)
	{
		var animalFosterMaps = await animalFosterMapRepository
			.GetAnimalFosterMapsForFoster(request.FosterId, cancellationToken);
		var result = animalFosterMaps.Select(a => new AnimalFosterMapDto
		{
			Id = a.Id,
			AnimalId = a.AnimalId,
			FosterId = a.FosterId.ToString(),
			StartFosteringDate = a.StartFosteringDate,
			EndFosteringDate = a.EndFosteringDate,
			SupervisingComment = a.SupervisingComment
		}).ToList();

		return new CollectionResponse<AnimalFosterMapDto>(result, result.Count);
	}

	public async Task<CollectionResponse<AnimalFosterMapDto>> Handle(GetAnimalFosterMapsByAnimalId request, CancellationToken cancellationToken)
	{
		var animalFosterMaps = await animalFosterMapRepository
			.GetAnimalFosterMapsForAnimal(request.AnimalId, cancellationToken);
		var result = animalFosterMaps.Select(a => new AnimalFosterMapDto
		{
			Id = a.Id,
			AnimalId = a.AnimalId,
			FosterId = a.FosterId.ToString(),
			StartFosteringDate = a.StartFosteringDate,
			EndFosteringDate = a.EndFosteringDate,
			SupervisingComment = a.SupervisingComment
		}).ToList();

		return new CollectionResponse<AnimalFosterMapDto>(result, result.Count);
	}

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
		query = query.ApplyFilter(request);
		IQueryable<AnimalFosterMapOverview> overview = query.ProjectToOverview();
		overview = overview
			.SortAndPaginate(request.SortBy, request.SortOrder, request.Skip, request.Take);
		List<AnimalFosterMapOverview> animalFosterMapOverviewsList = await overview.ToListAsync(cancellationToken);
		return new CollectionResponse<AnimalFosterMapOverview>(animalFosterMapOverviewsList, animalFosterMapOverviewsList.Count);

	}
}