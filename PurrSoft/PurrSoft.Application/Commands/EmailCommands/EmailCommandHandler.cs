using MediatR;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Application.Interfaces;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Commands.EmailCommands;

internal class EmailCommandHandler(IEmailService emailService,
    ILogRepository<EmailCommandHandler> _logRepository)
    : IRequestHandler<SendEmailCommand, CommandResponse>
{
    public async Task<CommandResponse> Handle(SendEmailCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await emailService.SendEmailAsync(email: request.Email);
            return CommandResponse.Ok();
        }
        catch (Exception e)
        {
            _logRepository.LogException(LogLevel.Error, e);
            throw;
        }
    }
}
