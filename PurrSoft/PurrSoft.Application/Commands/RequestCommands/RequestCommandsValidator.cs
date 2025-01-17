using FluentValidation;
using PurrSoft.Application.Commands.ShiftCommands;
using PurrSoft.Domain.Entities.Enums;

namespace PurrSoft.Application.Commands.RequestCommands;

public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
{
	public CreateRequestCommandValidator()
	{
		RuleFor(x => x.RequestDto.Description)
			.NotNull().NotEmpty();
		RuleFor(x => x.RequestDto.RequestType)
			.NotNull().NotEmpty()
			.Must(x => Enum.IsDefined(typeof(RequestType), x))
			.WithMessage("RequestType must be a valid RequestType");
		RuleFor(x => x.RequestDto.CreatedAt)
			.NotNull().NotEmpty().GreaterThan(DateTime.MinValue);
		RuleFor(x => x.RequestDto.UserId)
			.NotNull().NotEmpty();
		RuleFor(x => x.RequestDto.Images)
			.NotEmpty()
			.When(x => x.RequestDto.Images != null);
	}
}

public class UpdateRequestCommandValidator : AbstractValidator<UpdateRequestCommand>
{
	public UpdateRequestCommandValidator()
	{
		RuleFor(x => x.RequestDto.Id)
			.NotNull().NotEmpty();
		RuleFor(x => x.RequestDto.Description)
			.NotNull().NotEmpty();
		RuleFor(x => x.RequestDto.RequestType)
			.NotNull().NotEmpty()
			.Must(x => Enum.IsDefined(typeof(RequestType), x));
		RuleFor(x => x.RequestDto.CreatedAt)
			.NotNull().NotEmpty().GreaterThan(DateTime.MinValue);
		RuleFor(x => x.RequestDto.UserId)
			.NotNull().NotEmpty();
	}
}

public class DeleteRequestCommandValidator : AbstractValidator<DeleteRequestCommand>
{
	public DeleteRequestCommandValidator()
	{
		RuleFor(x => x.Id)
			.NotNull().NotEmpty();
	}
}
