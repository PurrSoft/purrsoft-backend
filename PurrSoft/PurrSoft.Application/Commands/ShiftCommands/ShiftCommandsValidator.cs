using FluentValidation;
using PurrSoft.Domain.Entities.Enums;

namespace PurrSoft.Application.Commands.ShiftCommands;

public class CreateShiftCommandValidator : AbstractValidator<CreateShiftCommand>
{
	public CreateShiftCommandValidator()
	{
		RuleFor(x => x.ShiftDto.Start)
			.NotNull().NotEmpty().GreaterThan(DateTime.MinValue);
		RuleFor(x => x.ShiftDto.ShiftType)
			.NotNull().NotEmpty()
			.Must(x => Enum.IsDefined(typeof(ShiftType), x));
		RuleFor(x => x.ShiftDto.ShiftStatus)
			.NotNull().NotEmpty()
			.Must(x => Enum.IsDefined(typeof(ShiftStatus), x));
		RuleFor(x => x.ShiftDto.VolunteerId)
			.NotEmpty()
			.When(x => x.ShiftDto.VolunteerId != null);
	}
}

public class UpdateShiftCommandValidator : AbstractValidator<UpdateShiftCommand>
{
	public UpdateShiftCommandValidator()
	{
		RuleFor(x => x.ShiftDto.Id)
			.NotNull().NotEmpty();
		RuleFor(x => x.ShiftDto.Start)
			.NotNull().NotEmpty().GreaterThan(DateTime.MinValue);
		RuleFor(x => x.ShiftDto.ShiftType)
			.NotNull().NotEmpty()
			.Must(x => Enum.IsDefined(typeof(ShiftType), x));
		RuleFor(x => x.ShiftDto.ShiftStatus)
			.NotNull().NotEmpty()
			.Must(x => Enum.IsDefined(typeof(ShiftStatus), x));
		RuleFor(x => x.ShiftDto.VolunteerId)
			.NotEmpty()
			.When(x => x.ShiftDto.VolunteerId != null);
	}
}

public class DeleteShiftCommandValidator : AbstractValidator<DeleteShiftCommand>
{
	public DeleteShiftCommandValidator()
	{
		RuleFor(x => x.Id)
			.NotNull().NotEmpty();
	}
}
