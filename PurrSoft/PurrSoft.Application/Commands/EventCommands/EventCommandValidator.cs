using FluentValidation;
using PurrSoft.Application.Helpers;

namespace PurrSoft.Application.Commands.EventCommands;

public class EventCreateCommandValidator : AbstractValidator<CreateEventCommand>
{
    public EventCreateCommandValidator()
    {
        RuleFor(e => e.EventDto.Title).NotNull().NotEmpty();
    }
}

public class EventUpdateCommandValidator : AbstractValidator<UpdateEventCommand>
{
    public EventUpdateCommandValidator()
    {
        RuleFor(e => e.EventDto.Id)
            .NotNull().NotEmpty();
        RuleFor(e => e.EventDto.Title)
            .NotNull().NotEmpty();
    }
}

public class EventDeleteCommandValidator : AbstractValidator<DeleteEventCommand>
{
    public EventDeleteCommandValidator()
    {
        RuleFor(e => e.Id)
            .NotNull().NotEmpty()
            .Must(GuidValidationHelper.BeAValidGuid)
            .WithMessage("Id must be a valid GUID.");
    }
}


