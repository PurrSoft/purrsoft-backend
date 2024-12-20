﻿using FluentValidation;
using PurrSoft.Application.Helpers;
using PurrSoft.Domain.Entities.Enums;
using static PurrSoft.Application.Commands.FosterCommands.FosterCommands;

namespace PurrSoft.Application.Commands.FosterCommands;

public class CreateFosterCommandValidator : AbstractValidator<CreateFosterCommand>
{
	public CreateFosterCommandValidator()
	{
		RuleFor(x => x.FosterDto.UserId)
			.NotNull().NotEmpty();
		RuleFor(x => x.FosterDto.StartDate)
			.NotNull().NotEmpty().GreaterThan(DateTime.MinValue);
		RuleFor(x => x.FosterDto.Status)
			.NotNull().NotEmpty()
			.Must(x => Enum.IsDefined(typeof(FosterStatus), x));
		RuleFor(x => x.FosterDto.Location)
			.NotNull().NotEmpty();
		RuleFor(x => x.FosterDto.HomeDescription)
			.NotNull();
		RuleFor(x => x.FosterDto.HasOtherAnimals)
			.NotNull();
		RuleFor(x => x.FosterDto.AnimalFosteredCount)
			.NotNull().GreaterThanOrEqualTo(0);

	}
}

public class UpdateFosterCommandValidator : AbstractValidator<UpdateFosterCommand>
{
	public UpdateFosterCommandValidator()
	{
		RuleFor(x => x.FosterDto.UserId)
			.NotNull().NotEmpty();
		RuleFor(x => x.FosterDto.StartDate)
			.NotNull().NotEmpty().GreaterThan(DateTime.MinValue);
		RuleFor(x => x.FosterDto.EndDate)
			.NotEmpty()
			.Must((dto, endDate) =>
							DateTimeValidationHelper.BeLaterThan(dto.FosterDto.StartDate, endDate))
			.WithMessage("EndDate must be greater than StartDate.")
			.When(x => x.FosterDto.EndDate != null);
		RuleFor(x => x.FosterDto.Status)
			.NotNull().NotEmpty()
			.Must(x => Enum.IsDefined(typeof(FosterStatus), x));
		RuleFor(x => x.FosterDto.Location)
			.NotNull().NotEmpty();
		RuleFor(x => x.FosterDto.HomeDescription)
			.NotNull();
		RuleFor(x => x.FosterDto.HasOtherAnimals)
			.NotNull();
		RuleFor(x => x.FosterDto.AnimalFosteredCount)
			.NotNull().GreaterThanOrEqualTo(0);

	}
}

public class DeleteFosterCommandValidator : AbstractValidator<DeleteFosterCommand>
{
	public DeleteFosterCommandValidator()
	{
		RuleFor(x => x.Id)
			.NotNull().NotEmpty();
	}
}