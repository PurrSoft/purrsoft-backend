using System.Net;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Common;
using PurrSoft.Application.Queries.AnimalProfileQueries;
using PurrSoft.Application.Commands.AnimalProfileCommands;
using PurrSoft.Application.Models;

namespace PurrSoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalProfileController : BaseController
    {
        public AnimalProfileController()
        {
        }

        // Get all animal profiles with optional filters
        [HttpGet()]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CollectionResponse<AnimalProfileDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAnimalProfilesAsync([FromQuery] GetAnimalProfilesQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(AnimalProfileDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAnimalProfileByIdAsync(Guid id)
        {
            var response = await Mediator.Send(new GetAnimalProfileByIdQuery { AnimalId = id.ToString() });

            // Assuming the data is stored in a 'Result' property
            return response.IsValid 
                ? Ok(response.Result) 
                : NotFound(response);
        }


        // Create a new animal profile
        [HttpPost()]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CommandResponse<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAnimalProfileAsync([FromBody] AnimalProfileCommands.AnimalProfileCreateCommand command)
        {
            var response = await Mediator.Send(command);

            return response.IsValid ? Ok(response) : BadRequest(response);
        }

        // Update an animal profile
        [HttpPut()]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAnimalProfileAsync([FromBody] AnimalProfileCommands.AnimalProfileUpdateCommand command)
        {
            var response = await Mediator.Send(command);

            return response.IsValid ? Ok(response) : BadRequest(response);
        }

        // Delete an animal profile
        [HttpDelete("{id:guid}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAnimalProfileAsync(Guid id)
        {
            var command = new AnimalProfileCommands.AnimalProfileDeleteCommand { Id = id };
            var response = await Mediator.Send(command);

            return response.IsValid ? Ok(response) : BadRequest(response);
        }
    }
}
