using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Commands.ShiftCommands;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.Queries.ShiftQueries;
using PurrSoft.Application.QueryOverviews;
using PurrSoft.Application.QueryResponses;
using System.Net;

namespace PurrSoft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftController : BaseController
{
	[HttpGet()]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer, Admin")]
	[ProducesResponseType(typeof(CollectionResponse<ShiftOverview>), (int)HttpStatusCode.OK)]
	public async Task<CollectionResponse<ShiftOverview>> GetShifts([FromQuery] GetFilteredShiftsQueries getFilteredShiftsQueries)
	{
		return await Mediator.Send(getFilteredShiftsQueries, new CancellationToken());
	}

	[HttpGet("{id}")]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer, Admin")]
	[ProducesResponseType(typeof(ShiftDto), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	public async Task<IActionResult> GetShiftById([FromRoute] Guid Id)
	{
		try
		{
			ShiftDto? shiftDto = await Mediator.Send(new GetShiftQuery { Id = Id }, new CancellationToken());

			return shiftDto != null ? Ok(shiftDto) : NotFound();
		}
		catch (ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}

	[HttpGet("Volunteers")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer, Admin")]
    [ProducesResponseType(typeof(CollectionResponse<ShiftVolunteerDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
	public async Task<CollectionResponse<ShiftVolunteerDto>> GetShiftVolunteers([FromQuery] GetShiftVolunteersQuery query)
	{
        return await Mediator.Send(query, new CancellationToken());
    }

    [HttpPost()]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer, Admin")]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> CreateShift([FromBody] CreateShiftCommand createShiftCommand)
	{
		try
		{
			CommandResponse commandResponse = await Mediator.Send(createShiftCommand, new CancellationToken());

			return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
		}
		catch (ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}

	[HttpPut()]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer, Admin")]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	public async Task<IActionResult> UpdateShift([FromBody] UpdateShiftCommand updateShiftCommand)
	{
		try
		{
			CommandResponse? commandResponse = await Mediator.Send(updateShiftCommand, new CancellationToken());
			if (commandResponse == null)
			{
				return NotFound();
			}
			return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
		}
		catch (ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}

	[HttpDelete("{id}")]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer, Admin")]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> DeleteShift([FromRoute] string id)
	{
		try
		{
			CommandResponse commandResponse = await Mediator.Send(new DeleteShiftCommand { Id = id }, new CancellationToken());

			return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
		}
		catch (ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}

	[HttpGet("GetCountByDate")]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer, Admin")]
	[ProducesResponseType(typeof(ShiftCountByDateResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> GetShiftCountByDate([FromQuery] GetShiftCountQuery getShiftCountQuery)
	{
		try
		{
			ShiftCountByDateResponse shiftCountByDateResponse =
				await Mediator.Send(getShiftCountQuery, new CancellationToken());

			return Ok(shiftCountByDateResponse);
		}
		catch (ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}
}