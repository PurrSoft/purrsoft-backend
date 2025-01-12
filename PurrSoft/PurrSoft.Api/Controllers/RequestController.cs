using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Commands.RequestCommands;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.Queries.RequestQueries;
using PurrSoft.Application.QueryOverviews;
using System.Net;

namespace PurrSoft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestController : BaseController
{
	[HttpGet()]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer, Foster, Admin")]
	[ProducesResponseType(typeof(CollectionResponse<RequestOverview>), (int)HttpStatusCode.OK)]
	public async Task<CollectionResponse<RequestOverview>> GetRequests([FromQuery] GetFilteredRequestsQueries getFilteredRequestsQueries)
	{
		return await Mediator.Send(getFilteredRequestsQueries, new CancellationToken());
	}

	[HttpGet("{id}")]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer, Foster, Admin")]
	[ProducesResponseType(typeof(RequestDto), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	public async Task<IActionResult> GetRequestById([FromRoute] Guid Id)
	{
		try
		{
			RequestDto? requestDto = await Mediator.Send(new GetRequestQuery { Id = Id }, new CancellationToken());

			return requestDto != null ? Ok(requestDto) : NotFound();
		}
		catch (ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}

	[HttpPost()]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer, Foster, Admin")]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> CreateRequest([FromBody] CreateRequestCommand createRequestCommand)
	{
		try
		{
			CommandResponse commandResponse = await Mediator.Send(createRequestCommand, new CancellationToken());

			return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
		}
		catch (ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}

	[HttpPut()]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer, Foster, Admin")]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	public async Task<IActionResult> UpdateRequest([FromBody] UpdateRequestCommand updateRequestCommand)
	{
		try
		{
			CommandResponse? commandResponse = await Mediator.Send(updateRequestCommand, new CancellationToken());
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
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer, Foster, Admin")]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> DeleteRequest([FromRoute] string id)
	{
		try
		{
			CommandResponse commandResponse = await Mediator.Send(new DeleteRequestCommand { Id = id }, new CancellationToken());

			return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
		}
		catch (ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}
}