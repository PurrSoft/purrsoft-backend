using FluentValidation;

namespace PurrSoft.Application.Commands.AccountCommands;

public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
{
	public UpdateAccountCommandValidator()
	{
		RuleFor(x => x.ApplicationUserDto.Id)
			.NotNull()
			.NotEmpty();
		RuleFor(x => x.ApplicationUserDto.Email)
			.NotEmpty().EmailAddress()
			.When(x => x.ApplicationUserDto.Email != null);
		RuleFor(x => x.ApplicationUserDto.FirstName)
			.NotNull()
			.NotEmpty();
		RuleFor(x => x.ApplicationUserDto.LastName)
			.NotNull()
			.NotEmpty();
		RuleFor(x => x.ApplicationUserDto.Address)
			.NotEmpty()
			.When(x => x.ApplicationUserDto.Address != null);;
	}

	public class UpdateUserRolesCommandValidator : AbstractValidator<UpdateUserRolesCommand>
	{
		public UpdateUserRolesCommandValidator()
		{
			RuleFor(x => x.Id)
				.NotNull()
				.NotEmpty();
			RuleFor(x => x.Roles)
				.NotNull();
		}
	}
}
