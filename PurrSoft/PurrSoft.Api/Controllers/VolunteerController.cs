using AlbumStore.Application.Commands.VolunteerCommands;
using AlbumStore.Application.Queries.VolunteerQueries;
using AlbumStore.Application.QueryOverviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Common;
using System.Net;

namespace PurrSoft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VolunteerController : BaseController
{
    [HttpGet("")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(typeof(List<VolunteerOverview>), (int)HttpStatusCode.OK)]
    public async Task<CollectionResponse<VolunteerOverview>> GetVolunteers([FromQuery] GetFilteredVolunteersQueries query)
    {
        return await Mediator.Send(query, new CancellationToken());
    }

    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(typeof(VolunteerOverview), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetVolunteer(string id)
    {
        VolunteerOverview volunteerOverview = await Mediator.Send(new GetVolunteerQuery() { Id = id }, new CancellationToken());
        if (volunteerOverview == null)
        {
            return NotFound();
        }

        return Ok(volunteerOverview);
    }

    [HttpPost("")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateVolunteer([FromBody] CreateVolunteerCommand volunteerCommand)
    {
        CommandResponse commandResponse = await Mediator.Send(volunteerCommand, new CancellationToken());
        if (commandResponse.IsValid)
        {
            return Ok(commandResponse);
        }

        return BadRequest(commandResponse);
    }

    [HttpPut()]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateVolunteer([FromBody] UpdateVolunteerCommand updateVolunteerCommand)
    {
        CommandResponse commandResponse = await Mediator.Send(updateVolunteerCommand, new CancellationToken());
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

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteVolunteer(string id)
    {
        CommandResponse commandResponse = await Mediator.Send(new DeleteVolunteerCommand() { Id = id }, new CancellationToken());
        if (commandResponse.IsValid)
        {
            return Ok(commandResponse);
        }

        return BadRequest(commandResponse);
    }
}
