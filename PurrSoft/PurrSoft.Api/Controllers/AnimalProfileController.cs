using System.Net;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Common;
using PurrSoft.Application.Queries.AnimalProfileQueries;
using PurrSoft.Application.Commands.AnimalProfileCommands;
using PurrSoft.Application.Models;
using PurrSoft.Domain.Entities;

namespace PurrSoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalProfileController : BaseController
    {
        public AnimalProfileController()
        {
        }

        // Get all animal profiles
        [HttpGet()]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CollectionResponse<AnimalProfileDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<CollectionResponse<AnimalProfileDto>> GetAllAnimalProfilesAsync()
        {
            CollectionResponse<AnimalProfileDto> response = 
                await Mediator.Send(new GetAnimalProfilesQuery());

            return response;
        }

        // Get animal profile by Id
        [HttpGet("/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(AnimalProfile), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAnimalProfileByIdAsync(Guid id)
        {
            var response = await Mediator.Send(new GetAnimalProfileByIdQuery { Id = id.ToString() });

            return response.IsValid ? Ok(response) : NotFound(new CommandResponse(new List<ValidationFailure>
            {
                new("Id", "Animal profile not found.")
            }));
        }
        

        // Create a new animal profile
        [HttpPost (  )]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CommandResponse<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse<Guid>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAnimalProfileAsync([FromBody] AnimalProfileCommands.AnimalProfileCreateCommand command)
        {
            CommandResponse<Guid> response = await Mediator.Send(command);

            return response.IsValid ? Ok(response) : BadRequest(response);
        }

        // Update an animal profile
        [HttpPut()]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAnimalProfileAsync([FromBody] AnimalProfileCommands.AnimalProfileUpdateCommand command)
        {
            CommandResponse response = await Mediator.Send(command);

            return response.IsValid ? Ok(response) : BadRequest(response);
        }

        // Delete an animal profile
        [HttpDelete("/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAnimalProfileAsync(Guid id)
        {
            var command = new AnimalProfileCommands.AnimalProfileDeleteCommand { Id = id };
            CommandResponse response = await Mediator.Send(command);

            return response.IsValid ? Ok(response) : BadRequest(response);
        }

    }
}
