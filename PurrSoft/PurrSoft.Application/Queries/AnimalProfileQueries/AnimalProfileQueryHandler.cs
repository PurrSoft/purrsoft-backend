using MediatR;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews.Mappers;
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
            var query = animalProfileRepository.Query();

            if (!string.IsNullOrEmpty(request.CurrentDisease))
                query = query.Where(ap => ap.CurrentDisease == request.CurrentDisease);
            if (!string.IsNullOrEmpty(request.CurrentMedication))
                query = query.Where(ap => ap.CurrentMedication == request.CurrentMedication);
            if (!string.IsNullOrEmpty(request.PastDisease))
                query = query.Where(ap => ap.PastDisease == request.PastDisease);
            if (!string.IsNullOrEmpty(request.Contract))
                query = query.Where(ap => ap.Contract == request.Contract);
            if (!string.IsNullOrEmpty(request.Microchip))
                query = query.Where(ap => ap.Microchip == request.Microchip);
            if (!string.IsNullOrEmpty(request.ContractState))
                query = query.Where(ap => ap.ContractState.ToString() == request.ContractState);
            if (request.ShelterCheckIn != null)
                query = query.Where(ap => ap.ShelterCheckIn == request.ShelterCheckIn);
            if (request.IncludeRabiesVaccine)
                query = query.Where(ap => !string.IsNullOrEmpty(ap.RabiesVaccine));
            if (request.IncludeMultivalentVaccine)
                query = query.Where(ap => !string.IsNullOrEmpty(ap.MultivalentVaccine));

            var result = await query
                .ProjectToDto()
                .ToListAsync(cancellationToken);

            return new CollectionResponse<AnimalProfileDto>(result, result.Count);
        }

        public async Task<CommandResponse<AnimalProfileDto>> Handle(GetAnimalProfileByIdQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.AnimalId, out var profileId))
            {
                return CommandResponse.Failed<AnimalProfileDto>("Invalid Animal profile ID format.");
            }

            var profile = await animalProfileRepository
                .Query(x => x.AnimalId == profileId)
                .ProjectToDto()
                .FirstOrDefaultAsync(cancellationToken);

            return profile == null
                ? CommandResponse.Failed<AnimalProfileDto>("Animal profile not found.")
                : CommandResponse.Ok(profile);
        }
    }
}
