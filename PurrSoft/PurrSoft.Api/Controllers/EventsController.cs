using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews;
using System.Net;
using PurrSoft.Application.Queries.EventQueries;
using PurrSoft.Application.Commands.EventCommands;

namespace PurrSoft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : BaseController
{
    [HttpGet()]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Admin,Foster,Volunteer")]
    [ProducesResponseType(typeof(CollectionResponse<EventOverview>), (int)HttpStatusCode.OK)]
    public async Task<CollectionResponse<EventOverview>> GetEvents([FromQuery] GetFilteredEventsQueries query)
    {
        return await Mediator.Send(query, new CancellationToken());
    }

    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Admin")]
    [ProducesResponseType(typeof(EventDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> GetEvent(string id)
    {
        try
        {
            EventDto eventDto = await Mediator.Send(new GetEventByIdQuery { Id = id }, new CancellationToken());

            if (eventDto == null)
            {
                return NotFound();
            }

            return Ok(eventDto);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

    [HttpPost()]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Admin")]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreatEvent([FromBody] CreateEventCommand createEventCommand)
    {
        try
        {
            CommandResponse commandResponse = await Mediator.Send(createEventCommand, new CancellationToken());

            return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new CommandResponse(ex.Errors.ToList()));
        }
    }

    [HttpPut()]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Admin")]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> UpdateFoster([FromBody] UpdateEventCommand updateEventCommand)
    {
        try
        {
            CommandResponse commandResponse = await Mediator.Send(updateEventCommand, new CancellationToken());
            if (commandResponse == null)
            {
                return NotFound();
            }
            if (commandResponse.IsValid)
            {
                return Ok(commandResponse);
            }
            return BadRequest(commandResponse);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new CommandResponse(ex.Errors.ToList()));
        }
    }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Admin")]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteEvent(string id)
    {
        try
        {
            CommandResponse commandResponse = await Mediator.Send(new DeleteEventCommand { Id = id }, new CancellationToken());;

            return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new CommandResponse(ex.Errors.ToList()));
        }
    }
}
