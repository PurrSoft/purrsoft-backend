using System.Net;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Common;
using PurrSoft.Application.Queries.TreatmentQueries;
using PurrSoft.Application.Commands.TreatmentCommands;
using PurrSoft.Application.Models;

namespace PurrSoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentController : BaseController
    {
        public TreatmentController()
        {
        }

        // Get all treatments with optional filters
        [HttpGet()]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CollectionResponse<TreatmentDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllTreatmentsAsync([FromQuery] GetTreatmentsQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        // Get a treatment by its ID
        [HttpGet("{id:guid}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(TreatmentDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTreatmentByIdAsync(Guid id)
        {
            var response = await Mediator.Send(new GetTreatmentByIdQuery { Id = id });

            return response.IsValid
                ? Ok(response.Result)
                : NotFound(response);
        }

        // Create a new treatment
        [HttpPost()]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CommandResponse<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTreatmentAsync([FromBody] TreatmentCommands.TreatmentCreateCommand command)
        {
            var response = await Mediator.Send(command);

            return response.IsValid ? Ok(response) : BadRequest(response);
        }

        // Update an existing treatment
        [HttpPut()]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateTreatmentAsync([FromBody] TreatmentCommands.TreatmentUpdateCommand command)
        {
            var response = await Mediator.Send(command);

            return response.IsValid ? Ok(response) : BadRequest(response);
        }

        // Delete a treatment by its ID
        [HttpDelete("{id:guid}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteTreatmentAsync(Guid id)
        {
            var command = new TreatmentCommands.TreatmentDeleteCommand { Id = id };
            var response = await Mediator.Send(command);

            return response.IsValid ? Ok(response) : BadRequest(response);
        }
    }
}
