using FluentValidation;
using PurrSoft.Domain.Entities.Enum;

namespace PurrSoft.Application.Commands.VolunteerCommands;

public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(x => x.VolunteerDto.UserId)
            .NotNull().NotEmpty();
        RuleFor(x => DateTime.Parse(x.VolunteerDto.StartDate))
            .NotNull().NotEmpty()
            .GreaterThan(DateTime.MinValue);
        RuleFor(x => x.VolunteerDto.Status)
            .NotNull().NotEmpty()
            .Must(VolunteerCommandEnumValidator.BeAValidStatus);
        RuleFor(x => x.VolunteerDto.Tier)
            .NotNull().NotEmpty()
            .Must(VolunteerCommandEnumValidator.BeAValidTier);
    }
}

public class UpdateVolunteerCommandValidator : AbstractValidator<UpdateVolunteerCommand>
{
    public UpdateVolunteerCommandValidator()
    {
        RuleFor(x => x.VolunteerDto.UserId)
            .NotNull().NotEmpty();
        RuleFor(x => DateTime.Parse(x.VolunteerDto.StartDate))
            .NotNull().NotEmpty()
            .GreaterThan(DateTime.MinValue);
        RuleFor(x => DateTime.Parse(x.VolunteerDto.EndDate))
            .NotNull().NotEmpty()
            .GreaterThan(v => DateTime.Parse(v.VolunteerDto.StartDate))
            .When(v => v.VolunteerDto.EndDate != null);
        RuleFor(x => x.VolunteerDto.Status)
            .NotNull().NotEmpty()
            .Must(VolunteerCommandEnumValidator.BeAValidStatus);
        RuleFor(x => x.VolunteerDto.Tier)
            .NotNull().NotEmpty()
            .Must(VolunteerCommandEnumValidator.BeAValidTier);
        RuleFor(x => x.VolunteerDto.Tasks)
            .NotNull().NotEmpty();
    }
}

public class DeleteVolunteerCommandValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVolunteerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().NotEmpty();
    }
}

public static class VolunteerCommandEnumValidator
{
    public static bool BeAValidStatus(string status)
    {
        return Enum.TryParse(typeof(VolunteerStatus), status, true, out _);
    }

    public static bool BeAValidTier(string tier)
    {
        return Enum.TryParse(typeof(TierLevel), tier, true, out _);
    }
}
