using FluentValidation;
using static PurrSoft.Application.Commands.AnimalFosterMapCommands.AnimalFosterMapCommands;

namespace PurrSoft.Application.Commands.AnimalFosterMapCommands
{
	public class AddAnimalToFosterCommandValidator : AbstractValidator<AddAnimalToFosterCommand>
	{
		public AddAnimalToFosterCommandValidator()
		{
			RuleFor(x => x.AnimalFosterMapDto.FosterId)
				.NotNull().NotEmpty().WithMessage("FosterId is required.");
			RuleFor(x => x.AnimalFosterMapDto.AnimalId)
				.NotNull().NotEmpty().WithMessage("AnimalId is required.");
			//RuleFor(x => x.AnimalFosterMapDto.StartFosteringDate)
			//	.NotNull().NotEmpty().WithMessage("StartFosteringDate is required.");
			//RuleFor(x => x.AnimalFosterMapDto.EndFosteringDate)
			//	.GreaterThanOrEqualTo(x => x.AnimalFosterMapDto.StartFosteringDate).WithMessage("EndFosteringDate must be greater than or equal to StartFosteringDate.");
		}
	}

	public class UpdateAnimalFosterMapCommandValidator : AbstractValidator<UpdateAnimalFosterMapCommand>
	{
		public UpdateAnimalFosterMapCommandValidator()
		{
			RuleFor(x => x.AnimalFosterMapDto.FosterId)
				.NotNull().NotEmpty().WithMessage("FosterId is required.");
			RuleFor(x => x.AnimalFosterMapDto.AnimalId)
				.NotNull().NotEmpty().WithMessage("AnimalId is required.");
			//RuleFor(x => x.AnimalFosterMapDto.StartFosteringDate)
			//	.NotNull().NotEmpty().WithMessage("StartFosteringDate is required.");
			//RuleFor(x => x.AnimalFosterMapDto.EndFosteringDate)
			//	.GreaterThanOrEqualTo(x => x.AnimalFosterMapDto.StartFosteringDate).WithMessage("EndFosteringDate must be greater than or equal to StartFosteringDate.");
		}
	}

	public class RemoveAnimalFromFosterCommandValidator : AbstractValidator<RemoveAnimalFromFosterCommand>
	{
		public RemoveAnimalFromFosterCommandValidator()
		{
			RuleFor(x => x.AnimalFosterMapId)
				.NotNull().NotEmpty().WithMessage("AnimalFosterMapId is required.");
		}
	}

}
