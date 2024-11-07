using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Commands.AnimalCommands;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.Queries.AnimalQueries;
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

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CollectionResponse<AnimalDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<CollectionResponse<AnimalDto>> GetAnimalsAsync()
    {
        CollectionResponse<AnimalDto> commandResponse =
            await Mediator.Send(new GetAnimalsQuery());

        return commandResponse;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CommandResponse<AnimalDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<CommandResponse<AnimalDto>> GetAnimalByIdAsync()
    {
        CommandResponse<AnimalDto> commandResponse =
            await Mediator.Send(new GetAnimalByIdQuery());

        return commandResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(typeof(CommandResponse<int>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateAnimalAsync(AnimalCreateCommand animalCreateCommand)
    {
        CommandResponse<string> commandResponse = await Mediator.Send(animalCreateCommand);

        return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(typeof(CommandResponse<Animal>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateAnimalAsync(AnimalUpdateCommand animalUpdateCommand)
    {
        CommandResponse commandResponse = await Mediator.Send(animalUpdateCommand);

        return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(typeof(CommandResponse<Animal>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteAnimalAsync(AnimalDeleteCommand animalDeleteCommand)
    {
        CommandResponse commandResponse = await Mediator.Send(animalDeleteCommand);

        return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
    }
}
