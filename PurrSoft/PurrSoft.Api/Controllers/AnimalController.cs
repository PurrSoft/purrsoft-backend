using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Commands.AnimalCommands;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.Queries.AnimalQueries;
using PurrSoft.Application.QueryOverviews;
using PurrSoft.Domain.Entities;
using System.Net;

namespace PurrSoft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalController : BaseController
{
	public AnimalController()
	{
	}

    [HttpGet("")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CollectionResponse<AnimalOverview>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<CollectionResponse<AnimalOverview>> GetAnimals([FromQuery] GetFilteredAnimalsQueries query)
    {
        return await Mediator.Send(query, new CancellationToken());
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AnimalDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAnimal(string id)
    {
        AnimalDto animal = await Mediator.Send(new GetAnimalByIdQuery { Id = id }, new CancellationToken());
        if (animal == null)
        {
            return NotFound();
        }

        return Ok(animal);

    }

    [HttpPost("")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(CommandResponse<int>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateAnimalAsync(CreateAnimalCommand animalCreateCommand)
    {
      try {
        CommandResponse commandResponse = await Mediator.Send(animalCreateCommand, new CancellationToken());
			  return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
      }
      catch (FluentValidation.ValidationException ex)
      {
        return BadRequest(new CommandResponse(ex.Errors.ToList()));
      }
	  } 

    [HttpPut()]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(typeof(CommandResponse<Animal>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateAnimalAsync(UpdateAnimalCommand animalUpdateCommand)
    {
      try {
        CommandResponse commandResponse = await Mediator.Send(animalUpdateCommand, new CancellationToken());
        return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
      }
      catch (FluentValidation.ValidationException ex)
      {
        return BadRequest(new CommandResponse(ex.Errors.ToList()));
      }
	  }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(typeof(CommandResponse<Animal>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteAnimalAsync(string id)
    {
      try {
        CommandResponse commandResponse = await Mediator.Send(new DeleteAnimalCommand { Id = id}, new CancellationToken());

        return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
      }
      catch (FluentValidation.ValidationException ex)
      {
        return BadRequest(new CommandResponse(ex.Errors.ToList()));
      }
	  }
}
