using FluentValidation;

namespace PurrSoft.Application.Commands.NotificationsCommands
{
    public class NotificationCreateCommandValidator : AbstractValidator<NotificationCommands.NotificationCommands.NotificationCreateCommand>
    {
        public NotificationCreateCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .MaximumLength(50).WithMessage("UserId should not exceed 50 characters.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .MaximumLength(50).WithMessage("Type should not exceed 50 characters.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.")
                .MaximumLength(500).WithMessage("Message should not exceed 500 characters.");
        }
    }

    public class NotificationUpdateCommandValidator : AbstractValidator<NotificationCommands.NotificationCommands.NotificationUpdateCommand>
    {
        public NotificationUpdateCommandValidator()
        {
            RuleFor(x => x.NotificationId)
                .NotEmpty().WithMessage("NotificationId is required.");

            RuleFor(x => x.Type)
                .MaximumLength(50).WithMessage("Type should not exceed 50 characters.");

            RuleFor(x => x.Message)
                .MaximumLength(500).WithMessage("Message should not exceed 500 characters.");

            RuleFor(x => x.IsRead)
                .NotNull().WithMessage("IsRead cannot be null when specified.");
        }
    }

    public class NotificationDeleteCommandValidator : AbstractValidator<NotificationCommands.NotificationCommands.NotificationDeleteCommand>
    {
        public NotificationDeleteCommandValidator()
        {
            RuleFor(x => x.NotificationId)
                .NotEmpty().WithMessage("NotificationId is required.");
        }
    }
}