using MediatR;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Queries.TreatmentQueries
{
    public class TreatmentQueryHandler(IRepository<Treatment> treatmentRepository) :
        IRequestHandler<GetTreatmentsQuery, CollectionResponse<TreatmentDto>>,
        IRequestHandler<GetTreatmentByIdQuery, CommandResponse<TreatmentDto>>
    {
        public async Task<CollectionResponse<TreatmentDto>> Handle(GetTreatmentsQuery request, CancellationToken cancellationToken)
        {
            var query = treatmentRepository.Query();

            // Apply filters based on query parameters
            if (request.IdAnimal.HasValue)
                query = query.Where(t => t.IdAnimal == request.IdAnimal.Value);
            if (!string.IsNullOrEmpty(request.MedicationName))
                query = query.Where(t => t.MedicationName.Contains(request.MedicationName));
            if (request.TreatmentStart.HasValue)
                query = query.Where(t => t.TreatmentStart >= request.TreatmentStart.Value);
            if (request.TreatmentEnd.HasValue)
                query = query.Where(t => t.TreatmentEnd <= request.TreatmentEnd.Value);

            // Map results to DTO
            var result = await query
                .Select(t => MapToDto(t))
                .ToListAsync(cancellationToken);

            return new CollectionResponse<TreatmentDto>(result, result.Count);
        }

        public async Task<CommandResponse<TreatmentDto>> Handle(GetTreatmentByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve and map a single treatment
            var treatment = await treatmentRepository
                .Query(t => t.Id == request.Id)
                .Select(t => MapToDto(t))
                .FirstOrDefaultAsync(cancellationToken);

            return treatment == null
                ? CommandResponse.Failed<TreatmentDto>("Treatment not found.")
                : CommandResponse.Ok(treatment);
        }

        private static TreatmentDto MapToDto(Treatment treatment) => new()
        {
            Id = treatment.Id,
            IdAnimal = treatment.IdAnimal,
            ImageUrl = treatment.ImageUrl,
            MedicationName = treatment.MedicationName,
            MedicationTime = treatment.MedicationTime,
            ExtraInfo = treatment.ExtraInfo,
            TreatmentStart = treatment.TreatmentStart,
            TreatmentEnd = treatment.TreatmentEnd,
            TreatmentDays = treatment.TreatmentDays
        };
    }
}
