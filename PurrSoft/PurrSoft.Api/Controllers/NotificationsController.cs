using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Commands.NotificationCommands;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.Queries.NotificationsQueries;

namespace PurrSoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : BaseController
    {
        public NotificationsController()
        {
        }

        // Get all notifications with optional filters
        [HttpGet()]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CollectionResponse<NotificationsDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllNotificationsAsync([FromQuery] GetNotificationsQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        // Get a notification by ID
        [HttpGet("{id:guid}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(NotificationsDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetNotificationByIdAsync(Guid id)
        {
            var response = await Mediator.Send(new GetNotificationByIdQuery { NotificationId = id });

            return response.IsValid 
                ? Ok(response.Result) 
                : NotFound(response);
        }

        // Create a new notification
        [HttpPost()]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CommandResponse<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateNotificationAsync([FromBody] NotificationCommands.NotificationCreateCommand command)
        {
            var response = await Mediator.Send(command);

            return response.IsValid ? Ok(response) : BadRequest(response);
        }

        // Update a notification
        [HttpPut()]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateNotificationAsync([FromBody] NotificationCommands.NotificationUpdateCommand command)
        {
            var response = await Mediator.Send(command);

            return response.IsValid ? Ok(response) : BadRequest(response);
        }

        // Delete a notification
        [HttpDelete("{id:guid}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteNotificationAsync(Guid id)
        {
            var command = new NotificationCommands.NotificationDeleteCommand { NotificationId = id };
            var response = await Mediator.Send(command);

            return response.IsValid ? Ok(response) : BadRequest(response);
        }
    }
}
