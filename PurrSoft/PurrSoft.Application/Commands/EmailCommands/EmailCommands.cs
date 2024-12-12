using PurrSoft.Application.Common;
using PurrSoft.Application.Models;

namespace PurrSoft.Application.Commands.EmailCommands;

public class SendEmailCommand : BaseRequest<CommandResponse>
{
    public EmailDto Email { get; set; }
}
