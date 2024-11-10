using MediatR;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Application.Common;
using PurrSoft.Application.DTOs;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Queries.AnimalProfileQueries
{
    public class AnimalProfileQueryHandler(IRepository<AnimalProfile> animalProfileRepository) :
        IRequestHandler<GetAnimalProfilesQuery, CollectionResponse<AnimalProfileDto>>,
        IRequestHandler<GetAnimalProfileByIdQuery, CommandResponse<AnimalProfileDto>>
    {
        public async Task<CollectionResponse<AnimalProfileDto>> Handle(GetAnimalProfilesQuery request, CancellationToken cancellationToken)
        {
            var result = await animalProfileRepository
                .Query()
                .Select(ap => new AnimalProfileDto
                {
                    Id = ap.Id,
                    CurrentDisease = ap.CurrentDisease,
                    CurrentMedication = ap.CurrentMedication,
                    PastDisease = ap.PastDisease
                })
                .ToListAsync(cancellationToken);

            return new CollectionResponse<AnimalProfileDto>(result, result.Count);
        }

        public async Task<CommandResponse<AnimalProfileDto>> Handle(GetAnimalProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var profile = await animalProfileRepository
                .Query(x => x.Id.ToString() == request.Id)
                .Select(ap => new AnimalProfileDto
                {
                    Id = ap.Id,
                    CurrentDisease = ap.CurrentDisease,
                    CurrentMedication = ap.CurrentMedication,
                    PastDisease = ap.PastDisease
                })
                .FirstOrDefaultAsync(cancellationToken);

            return profile == null
                ? CommandResponse.Failed<AnimalProfileDto>(new[] { "Animal profile not found." })
                : CommandResponse.Ok(profile);
        }
    }
}
