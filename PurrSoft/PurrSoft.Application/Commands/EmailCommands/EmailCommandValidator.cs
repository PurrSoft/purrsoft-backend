using FluentValidation;

namespace PurrSoft.Application.Commands.EmailCommands;

public class EmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    public EmailCommandValidator()
    {
        RuleFor(x => x.Email.To).NotEmpty().EmailAddress();
        RuleFor(x => x.Email.Subject).NotEmpty();
        RuleFor(x => x.Email.Body).NotEmpty();
    }

}
