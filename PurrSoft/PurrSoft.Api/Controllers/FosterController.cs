using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.Queries.FosterQueries;
using PurrSoft.Application.QueryOverviews;
using System.Net;
using static PurrSoft.Application.Commands.FosterCommands.FosterCommands;

namespace PurrSoft.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FosterController : BaseController
	{
		[HttpGet("")]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager")]
		[ProducesResponseType(typeof(CollectionResponse<FosterDto>), (int)HttpStatusCode.OK)]
		public async Task<CollectionResponse<FosterOverview>> GetFosters([FromQuery] GetFilteredFostersQueries query)
		{
			return await Mediator.Send(query, new CancellationToken());
		}

		[HttpGet("{id}")]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Foster")]
		[ProducesResponseType(typeof(FosterDto), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		public async Task<IActionResult> GetFoster(string id)
		{
			try
			{
				FosterOverview foster = await Mediator.Send(new GetFosterByIdQuery { Id = id }, new CancellationToken());

				if (foster == null)
				{
					return NotFound();
				}

				return Ok(foster);
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
		public async Task<IActionResult> CreateFoster([FromBody] CreateFosterCommand createFosterCommand)
		{
			CommandResponse commandResponse = await Mediator.Send(createFosterCommand, new CancellationToken());

			return commandResponse.IsValid ? Ok(commandResponse) : BadRequest();
		}

		[HttpPut()]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Foster")]
		[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		public async Task<IActionResult> UpdateFoster([FromBody] UpdateFosterCommand updateFosterCommand)
		{
			try
			{
				CommandResponse commandResponse = await Mediator.Send(updateFosterCommand, new CancellationToken());
				if (commandResponse == null)
				{
					return NotFound();
				}
				if (commandResponse.IsValid)
				{
					return Ok(commandResponse);
				}
				return BadRequest();
			}
			catch (UnauthorizedAccessException)
			{
				return Forbid();
			}
		}
		[HttpDelete("{id}")]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager")]
		[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> DeleteFoster(string id)
		{
			CommandResponse commandResponse = await Mediator.Send(new DeleteFosterCommand { Id = id }, new CancellationToken());

			return commandResponse.IsValid ? Ok(commandResponse) : BadRequest();
		}
	}
}
