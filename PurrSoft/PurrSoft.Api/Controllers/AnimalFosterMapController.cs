using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.Queries.AnimalFosterMapQueries;
using PurrSoft.Application.Queries.FosterQueries;
using PurrSoft.Application.QueryOverviews;
using System.Net;
using static PurrSoft.Application.Commands.AnimalFosterMapCommands.AnimalFosterMapCommands;

namespace PurrSoft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalFosterMapController : BaseController
{
	[HttpGet()]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Foster")]
	[ProducesResponseType(typeof(CollectionResponse<AnimalFosterMapDto>), (int)HttpStatusCode.OK)]
	public async Task<CollectionResponse<AnimalFosterMapOverview>> GetAnimalFosterMaps([FromQuery] GetFilteredAnimalFosterMapsQueries query)
	{
		return await Mediator.Send(query, new CancellationToken());
	}

	[HttpGet("{id}")]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Foster")]
	[ProducesResponseType(typeof(AnimalFosterMapDto), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	public async Task<IActionResult> GetAnimalFosterMapById(string id)
	{
		try
		{
			AnimalFosterMapDto? animalFosterMapDto = await Mediator.Send(new GetAnimalFosterMapById { Id = id }, new CancellationToken());

			if (animalFosterMapDto == null)
			{
				return NotFound();
			}

			return Ok(animalFosterMapDto);
		}
		catch (ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}

	[HttpPost()]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Foster")]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> AddAnimalFosterMap([FromBody] AddAnimalToFosterCommand addAnimalToFosterCommand)
	{
		try
		{
			CommandResponse commandResponse = await Mediator.Send(addAnimalToFosterCommand, new CancellationToken());

			return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
		}
		catch (ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}

	[HttpPut()]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Foster")]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> UpdateAnimalFosterMap([FromBody] UpdateAnimalFosterMapCommand updateAnimalFosterMapCommand)
	{
		try
		{
			CommandResponse commandResponse = await Mediator.Send(updateAnimalFosterMapCommand,
				new CancellationToken());

			return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
		}
		catch (ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}

	[HttpDelete("{id}")]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Foster")]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> RemoveAnimalFosterMap([FromRoute] string id)
	{
		try
		{
			CommandResponse commandResponse = await Mediator.Send(new RemoveAnimalFromFosterCommand { AnimalFosterMapId = id }, new CancellationToken());

			return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
		}
		catch (ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}

	[HttpGet("Foster/{fosterId}")]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Foster")]
	[ProducesResponseType(typeof(CollectionResponse<AnimalFosterMapDto>), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> GetAnimalFosterMapByFosterId([FromRoute] string fosterId)
	{
		CollectionResponse<AnimalFosterMapDto> commandResponse = await Mediator.Send(new GetAnimalFosterMapsByFosterId { FosterId = fosterId }, new CancellationToken());

		return Ok(commandResponse);
	}

	[HttpGet("Animal/{animalId}")]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager, Foster")]
	[ProducesResponseType(typeof(CollectionResponse<AnimalFosterMapDto>), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> GetAnimalFosterMapByAnimalId([FromRoute] string animalId)
	{
		CollectionResponse<AnimalFosterMapDto> commandResponse = await Mediator.Send(new GetAnimalFosterMapsByAnimalId { AnimalId = animalId }, new CancellationToken());

		return Ok(commandResponse);
	}


}