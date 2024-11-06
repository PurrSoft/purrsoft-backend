using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Commands.AnimalProfileCommands;
using PurrSoft.Application.Common;
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

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CollectionResponse<AnimalProfile>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<CollectionResponse<AnimalProfile>> GetAnimalProfileAsync()
        {
            CollectionResponse<AnimalProfile> response = 
                await Mediator.Send(new AnimalProfileCommands.AnimalProfileGetCommand());

            return response;
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CommandResponse<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse<Guid>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAnimalProfileAsync([FromBody] AnimalProfileCommands.AnimalProfileCreateCommand command)
        {
            CommandResponse<Guid> response = await Mediator.Send(command);

            return  response.IsValid ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAnimalProfileAsync([FromBody] AnimalProfileCommands.AnimalProfileUpdateCommand command)
        {
            CommandResponse response = await Mediator.Send(command);

            return  response.IsValid ? Ok(response) : BadRequest(response);

        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAnimalProfileAsync([FromBody] AnimalProfileCommands.AnimalProfileDeleteCommand command)
        {
            CommandResponse response = await Mediator.Send(command);

           return  response.IsValid ? Ok(response) : BadRequest(response);
        }
    }
}
