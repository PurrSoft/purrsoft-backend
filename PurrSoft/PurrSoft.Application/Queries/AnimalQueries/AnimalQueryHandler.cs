using MediatR;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews;
using PurrSoft.Application.QueryOverviews.Mappers;
using PurrSoft.Common.Helpful;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Queries.AnimalQueries;

public class AnimalQueryHandler(IRepository<Animal> animalRepository) :
    IRequestHandler<GetFilteredAnimalsQueries, CollectionResponse<AnimalOverview>>,
    IRequestHandler<GetAnimalByIdQuery, AnimalDto>
{

    public async Task<AnimalDto> Handle(GetAnimalByIdQuery request, CancellationToken cancellationToken)
    {
        var animal = await animalRepository
            .Query(x => x.Id.ToString() == request.Id)
            .Select(a => new AnimalDto
            {
                Id = a.Id.ToString(),
                Name = a.Name,
                AnimalType = a.AnimalType.ToString(),
                Gender = a.Gender,
                YearOfBirth = a.YearOfBirth,
                Sterilized = a.Sterilized,
                ImageUrl = a.ImageUrl
            })
            .FirstOrDefaultAsync(cancellationToken);

        return animal;
    }

    async Task<CollectionResponse<AnimalOverview>> IRequestHandler<GetFilteredAnimalsQueries, CollectionResponse<AnimalOverview>>.Handle(GetFilteredAnimalsQueries request, CancellationToken cancellationToken)
    {
        IQueryable<Animal> query = animalRepository.Query();
        query = query.ApplyFilter(request);
        int totalNumberOfItems = await query.CountAsync(cancellationToken);
        IQueryable<AnimalOverview> animalOverviews = query.ProjectToOverview();
        animalOverviews = animalOverviews
            .SortAndPaginate(request.SortBy, request.SortOrder, request.Skip, request.Take);
        List<AnimalOverview> animalOverviewsList =
            await animalOverviews.ToListAsync(cancellationToken);

        return new CollectionResponse<AnimalOverview>(
            animalOverviewsList,
            totalNumberOfItems);
    }
}