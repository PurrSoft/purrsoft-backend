using MediatR;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
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

            // Apply filters based on query parameters
            if (!string.IsNullOrEmpty(request.CurrentDisease))
                query = query.Where(ap => ap.CurrentDisease == request.CurrentDisease);
            if (!string.IsNullOrEmpty(request.CurrentMedication))
                query = query.Where(ap => ap.CurrentMedication == request.CurrentMedication);
            if (!string.IsNullOrEmpty(request.PastDisease))
                query = query.Where(ap => ap.PastDisease == request.PastDisease);
            if (!string.IsNullOrEmpty(request.Passport))
                query = query.Where(ap => ap.Passport == request.Passport); // "Carnet" mapped to "Passport"
            if (!string.IsNullOrEmpty(request.Microchip))
                query = query.Where(ap => ap.Microchip == request.Microchip); // "Microcip" mapped to "Microchip"

            // Map results to DTO
            var result = await query
                .Select(ap => MapToDto(ap))
                .ToListAsync(cancellationToken);

            return new CollectionResponse<AnimalProfileDto>(result, result.Count);
        }

        public async Task<CommandResponse<AnimalProfileDto>> Handle(GetAnimalProfileByIdQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.AnimalId, out var profileId))
            {
                return CommandResponse.Failed<AnimalProfileDto>(new[] { "Invalid Animal profile ID format." });
            }

            // Retrieve and map a single profile
            var profile = await animalProfileRepository
                .Query(x => x.AnimalId == profileId)
                .Select(ap => MapToDto(ap))
                .FirstOrDefaultAsync(cancellationToken);

            return profile == null
                ? CommandResponse.Failed<AnimalProfileDto>(new[] { "Animal profile not found." })
                : CommandResponse.Ok(profile);
        }
        
        private static AnimalProfileDto MapToDto(AnimalProfile ap) => new AnimalProfileDto
        {
            AnimalId = ap.AnimalId,
            Passport = ap.Passport,
            Microchip = ap.Microchip,
            CurrentDisease = ap.CurrentDisease,
            CurrentMedication = ap.CurrentMedication,
            PastDisease = ap.PastDisease,
            CurrentTreatment = ap.CurrentTreatment,
            RabiesVaccine = ap.RabiesVaccine,
            MultivalentVaccine = ap.MultivalentVaccine,
            UsefulLinks = ap.UsefulLinks
        };
    }
}
