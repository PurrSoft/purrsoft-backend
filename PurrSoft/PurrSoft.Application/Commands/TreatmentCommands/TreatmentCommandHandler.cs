using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Commands.TreatmentCommands
{
    public class TreatmentCommandHandler(
        IRepository<Treatment> treatmentRepository,
        IRepository<Animal> animalRepository,
        ILogRepository<TreatmentCommandHandler> logRepository)
        : IRequestHandler<TreatmentCommands.TreatmentCreateCommand, CommandResponse<Guid>>,
          IRequestHandler<TreatmentCommands.TreatmentUpdateCommand, CommandResponse>,
          IRequestHandler<TreatmentCommands.TreatmentDeleteCommand, CommandResponse>
    {
        public async Task<CommandResponse<Guid>> Handle(TreatmentCommands.TreatmentCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var animal = await animalRepository.Query(x => x.Id == request.IdAnimal).FirstOrDefaultAsync(cancellationToken);
                if (animal == null)
                {
                    return CommandResponse.Failed(new List<ValidationFailure>
                    {
                        new("IdAnimal", "Animal not found.")
                    }) as CommandResponse<Guid> ?? throw new InvalidOperationException();
                }

                var newTreatment = new Treatment
                {
                    IdAnimal = request.IdAnimal,
                    ImageUrl = request.ImageUrl,
                    MedicationName = request.MedicationName,
                    MedicationTime = request.MedicationTime,
                    ExtraInfo = request.ExtraInfo,
                    TreatmentStart = request.TreatmentStart,
                    TreatmentEnd = request.TreatmentEnd,
                    TreatmentDays = request.TreatmentDays
                };

                treatmentRepository.Add(newTreatment);
                await treatmentRepository.SaveChangesAsync(cancellationToken);

                return CommandResponse.Ok(newTreatment.Id);
            }
            catch (Exception ex)
            {
                logRepository.LogException(LogLevel.Error, ex);
                throw;
            }
        }

        public async Task<CommandResponse> Handle(TreatmentCommands.TreatmentUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var treatment = await treatmentRepository
                    .Query(x => x.Id == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                if (treatment == null)
                {
                    return CommandResponse.Failed(new List<ValidationFailure>
                    {
                        new("Id", "Treatment not found.")
                    });
                }

                treatment.ImageUrl = request.ImageUrl ?? treatment.ImageUrl;
                treatment.MedicationName = request.MedicationName;
                treatment.MedicationTime = request.MedicationTime;
                treatment.ExtraInfo = request.ExtraInfo ?? treatment.ExtraInfo;
                treatment.TreatmentStart = request.TreatmentStart;
                treatment.TreatmentEnd = request.TreatmentEnd;
                treatment.TreatmentDays = request.TreatmentDays;

                await treatmentRepository.SaveChangesAsync(cancellationToken);

                return CommandResponse.Ok();
            }
            catch (Exception ex)
            {
                logRepository.LogException(LogLevel.Error, ex);
                return CommandResponse.Failed(new List<ValidationFailure>
                {
                    new("Treatment", "Failed to update treatment.")
                });
            }
        }

        public async Task<CommandResponse> Handle(TreatmentCommands.TreatmentDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var treatment = await treatmentRepository
                    .Query(x => x.Id == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                if (treatment == null)
                {
                    return CommandResponse.Failed(new List<ValidationFailure>
                    {
                        new("Id", "Treatment not found.")
                    });
                }

                treatmentRepository.Remove(treatment);
                await treatmentRepository.SaveChangesAsync(cancellationToken);

                return CommandResponse.Ok();
            }
            catch (Exception ex)
            {
                logRepository.LogException(LogLevel.Error, ex);
                return CommandResponse.Failed(new List<ValidationFailure>
                {
                    new("Treatment", "Failed to delete treatment.")
                });
            }
        }
    }
}
