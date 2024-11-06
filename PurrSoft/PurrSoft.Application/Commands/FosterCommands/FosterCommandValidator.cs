using FluentValidation;
using PurrSoft.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PurrSoft.Application.Commands.FosterCommands.FosterCommands;

namespace PurrSoft.Application.Commands.FosterCommands
{
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
				.NotNull();
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
				.GreaterThan(DateTime.MinValue)
				.When(x => x.FosterDto.EndDate != null);
			RuleFor(x => x.FosterDto.Status)
				.NotNull().NotEmpty()
				.Must(x => Enum.IsDefined(typeof(FosterStatus), x));
			RuleFor(x => x.FosterDto.Location)
				.NotNull();
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
}
