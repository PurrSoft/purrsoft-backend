using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Commands.ShiftCommands;
using PurrSoft.Application.Common;
using System.Net;

namespace PurrSoft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftController : BaseController
{
	[HttpPost()]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer")]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> CreateShift([FromBody] CreateShiftCommand createShiftCommand)
	{
		try
		{
			CommandResponse commandResponse = await Mediator.Send(createShiftCommand, new CancellationToken());

			return commandResponse.IsValid ? Ok(commandResponse) : BadRequest();
		}
		catch (ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}

	[HttpPut()]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer")]
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
			return commandResponse.IsValid ? Ok(commandResponse) : BadRequest();
		}
		catch (ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}

	[HttpDelete("{id}")]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer")]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> DeleteShift(string id)
	{
		try
		{
			CommandResponse commandResponse = await Mediator.Send(new DeleteShiftCommand { Id = id }, new CancellationToken());

			return commandResponse.IsValid ? Ok(commandResponse) : BadRequest();
		}
		catch (ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}
}