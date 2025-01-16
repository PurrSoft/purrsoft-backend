using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Commands.AccountCommands;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.Queries.AccountQueries;
using System.Net;

namespace PurrSoft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : BaseController
{
	public AccountController()
	{

	}

	[HttpGet]
	[Authorize(AuthenticationSchemes = "Bearer")]
	[ProducesResponseType(typeof(ApplicationUserDto), (int)HttpStatusCode.OK)]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
	public async Task<ApplicationUserDto> GetLoggedInUser()
	{
		ApplicationUserDto commandResponse =
			await Mediator.Send(new GetLoggedInUserQuery());

		return commandResponse;
	}

	[HttpGet("{id}/GetRolesAndStatuses")]
	[Authorize(AuthenticationSchemes = "Bearer")]
	[ProducesResponseType(typeof(CollectionResponse<UserRoleStatusDto>), (int)HttpStatusCode.OK)]
	public async Task<CollectionResponse<UserRoleStatusDto>> GetRolesAndStatusesByUserId([FromRoute] string id)
		=> await Mediator.Send(new GetRolesAndStatusesByUserIdQuery() { Id = id });

	[HttpGet("{id}/GetRolesAndDates")]
	[Authorize(AuthenticationSchemes = "Bearer")]
	[ProducesResponseType(typeof(CollectionResponse<UserRoleDatesDto>), (int)HttpStatusCode.OK)]
	public async Task<CollectionResponse<UserRoleDatesDto>> GetRolesAndDatesByUserId([FromRoute] string id)
		=> await Mediator.Send(new GetRolesAndDatesByUserIdQuery() { Id = id });

	[HttpGet("GetUsersByRole/{role}")]
	[Authorize(AuthenticationSchemes = "Bearer")]
	[ProducesResponseType(typeof(ApplicationUserDto), (int)HttpStatusCode.OK)]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
	public async Task<CollectionResponse<ApplicationUserDto>> GetUsersByRole([FromRoute] string role)
		=> await Mediator.Send(new GetUsersByRoleQuery() { Role = role });

	[HttpPut()]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer, Foster")]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
	[ProducesResponseType((int)HttpStatusCode.Forbidden)]
	public async Task<IActionResult> UpdateUser([FromBody] UpdateAccountCommand command)
	{
		try
		{
			CommandResponse commandResponse = await Mediator.Send(command, new CancellationToken());

			if (commandResponse == null)
			{
				return NotFound(commandResponse);
			}
			if (commandResponse.IsValid)
			{
				return Ok(commandResponse);
			}
			return BadRequest(commandResponse);
		}
		catch (UnauthorizedAccessException ex)
		{
			return StatusCode((int)HttpStatusCode.Forbidden, ex.Message);
		}
		catch (ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}

	[HttpPut("{userId}/Roles")]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager")]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
	[ProducesResponseType((int)HttpStatusCode.Forbidden)]
	public async Task<IActionResult> UpdateUserRole([FromRoute] string userId, [FromBody] UpdateUserRolesCommand command)
	{
		try
		{
			command.Id = userId;
			CommandResponse commandResponse = await Mediator.Send(command, new CancellationToken());

			if (commandResponse == null)
			{
				return NotFound(commandResponse);
			}
			if (commandResponse.IsValid)
			{
				return Ok(commandResponse);
			}
			return BadRequest(commandResponse);
		}
		catch (UnauthorizedAccessException ex)
		{
			return StatusCode((int)HttpStatusCode.Forbidden, ex.Message);
		}
		catch (ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}
}
