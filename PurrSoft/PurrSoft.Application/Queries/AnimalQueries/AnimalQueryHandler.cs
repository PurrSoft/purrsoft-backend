using MediatR;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews.Mappers;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Queries.AnimalQueries;

public class AnimalQueryHandler(IRepository<Animal> animalRepository) :
    IRequestHandler<GetAnimalsQuery, CollectionResponse<AnimalDto>>,
    IRequestHandler<GetAnimalByIdQuery, CommandResponse<AnimalDto>>
{
    public async Task<CollectionResponse<AnimalDto>> Handle(GetAnimalsQuery request, CancellationToken cancellationToken)
    {
        var result =  await animalRepository
            .Query()
            .Select(a => new AnimalDto
            {
                Id = a.Id.ToString(),
                Name = a.Name,
                AnimalType = a.AnimalType.ToString(),
                Gender = a.Gender,
                YearOfBirth = a.YearOfBirth,
                Sterilized = a.Sterilized,
            })
            .ToListAsync(cancellationToken);
        return new CollectionResponse<AnimalDto>(result, result.Count);
    }

    public async Task<CommandResponse<AnimalDto>> Handle(GetAnimalByIdQuery request, CancellationToken cancellationToken)
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
            })
            .FirstOrDefaultAsync(cancellationToken);

        return animal == null
            ? CommandResponse<AnimalDto>.Failed<AnimalDto>(new []{ "Animal not found."})
            : CommandResponse.Ok(animal);
    }
}