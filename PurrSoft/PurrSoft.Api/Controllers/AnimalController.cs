using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Commands.AnimalCommands;
using PurrSoft.Application.Common;
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
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(typeof(ICollection<Animal>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<CollectionResponse<Animal>> GetAnimalsAsync()
    {
        CollectionResponse<Animal> commandResponse =
            await Mediator.Send(new AnimalGetCommand());

        return commandResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(typeof(CommandResponse<int>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateAnimalAsync(AnimalCreateCommand animalCreateCommand)
    {
        CommandResponse<int> commandResponse = await Mediator.Send(animalCreateCommand);

        return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(typeof(CommandResponse<Animal>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateAnimalAsync(AnimalUpdateCommand animalUpdateCommand)
    {
        CommandResponse<Animal> commandResponse = await Mediator.Send(animalUpdateCommand);

        return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(typeof(CommandResponse<Animal>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteAnimalAsync(AnimalDeleteCommand animalDeleteCommand)
    {
        CommandResponse<Animal> commandResponse = await Mediator.Send(animalDeleteCommand);

        return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
    }
}
