using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Commands.EmailCommands;
using PurrSoft.Application.Common;
using System.Net;

namespace PurrSoft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController : BaseController
{
    public EmailController()
    {
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager")]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> SendEmail([FromForm] SendEmailCommand sendEmailCommand)
    {
        Console.WriteLine(sendEmailCommand);
        CommandResponse commandResponse =
            await Mediator.Send(sendEmailCommand,
                new CancellationToken());
        return commandResponse.IsValid ? Ok(commandResponse)
            : BadRequest(commandResponse);
    }

}

