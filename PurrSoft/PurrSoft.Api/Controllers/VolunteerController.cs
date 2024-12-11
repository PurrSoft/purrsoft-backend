using PurrSoft.Application.Commands.VolunteerCommands;
using PurrSoft.Application.Queries.VolunteerQueries;
using PurrSoft.Application.QueryOverviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Common;
using System.Net;
using PurrSoft.Application.Models;

namespace PurrSoft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VolunteerController : BaseController
{
	[HttpGet("")]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager")]
	[ProducesResponseType(typeof(CollectionResponse<VolunteerOverview>), (int)HttpStatusCode.OK)]
	public async Task<CollectionResponse<VolunteerOverview>> GetVolunteers([FromQuery] GetFilteredVolunteersQueries query)
	{
		return await Mediator.Send(query, new CancellationToken());
	}

	[HttpGet("{id}")]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer")]
	[ProducesResponseType(typeof(VolunteerDto), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	[ProducesResponseType((int)HttpStatusCode.Forbidden)]
	public async Task<IActionResult> GetVolunteer(string id)
	{
		try
		{
			VolunteerDto volunteerDto =
				await Mediator.Send(new GetVolunteerQuery() { Id = id }, new CancellationToken());

			if (volunteerDto == null)
			{
				return NotFound();
			}

			return Ok(volunteerDto);
		}
		catch (UnauthorizedAccessException)
		{
			return Forbid();
		}
	}

	[HttpPost("")]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager")]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> CreateVolunteer([FromBody] CreateVolunteerCommand volunteerCommand)
	{
		try
		{
			CommandResponse commandResponse = await Mediator.Send(volunteerCommand, new CancellationToken());
			if (commandResponse.IsValid)
			{
				return Ok(commandResponse);
			}

			return BadRequest(commandResponse);
		}
		catch (FluentValidation.ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}

	[HttpPut()]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer")]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	[ProducesResponseType((int)HttpStatusCode.Forbidden)]
	public async Task<IActionResult> UpdateVolunteer([FromBody] UpdateVolunteerCommand updateVolunteerCommand)
	{
		try
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
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager")]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> DeleteVolunteer(string id)
	{
		try
		{
			CommandResponse commandResponse =
				await Mediator.Send(new DeleteVolunteerCommand() { Id = id }, new CancellationToken());
			if (commandResponse.IsValid)
			{
				return Ok(commandResponse);
			}

			return BadRequest(commandResponse);
		}
		catch (FluentValidation.ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}
}
