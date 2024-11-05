using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.Queries.AccountQueries;
using PurrSoft.Application.Queries.FosterQueries;
using System.Net;
using static PurrSoft.Application.Commands.FosterCommands.FosterCommands;

namespace PurrSoft.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FosterController : BaseController
	{
		[HttpGet("")]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer")]
		[ProducesResponseType(typeof(CollectionResponse<FosterDto>), (int)HttpStatusCode.OK)]
		public async Task<CollectionResponse<FosterDto>> GetFosters([FromQuery] GetFilteredFostersQueries query)
		{
			return await Mediator.Send(query, new CancellationToken());
		}

		[HttpGet("{id}")]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Volunteer, Foster")]
		[ProducesResponseType(typeof(FosterDto), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		public async Task<IActionResult> GetFoster(string id)
		{
			try
			{
				FosterDto foster = await Mediator.Send(new GetFosterByIdQuery { Id = id }, new CancellationToken());

				if(foster == null)
				{
					return NotFound();
				}

				return Ok(foster);
			} catch(UnauthorizedAccessException)
			{
				return Forbid();
			}
		}

		[HttpPost("")]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager")]
		[ProducesResponseType(typeof(FosterDto), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> CreateFoster(FosterDto foster)
		{
			CommandResponse commandResponse = await Mediator.Send(new CreateFosterCommand { FosterDto = foster }, new CancellationToken());

			return commandResponse.IsValid ? Ok(foster) : BadRequest(commandResponse);
		}

		[HttpPut("{id}")]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Foster")]
		[ProducesResponseType(typeof(FosterDto), (int)HttpStatusCode.OK)]
	}
}
