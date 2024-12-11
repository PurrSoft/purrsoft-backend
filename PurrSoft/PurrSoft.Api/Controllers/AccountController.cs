using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.Queries.AccountQueries;
using System.Net;

namespace PurrSoft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : BaseController
{
	public AccountController()
	{

	}

	[HttpGet]
	[Authorize(AuthenticationSchemes = "Bearer")]
	[ProducesResponseType(typeof(ApplicationUserDto), (int)HttpStatusCode.OK)]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
	public async Task<ApplicationUserDto> GetLoggedInUser()
	{
		ApplicationUserDto commandResponse =
			await Mediator.Send(new GetLoggedInUserQuery());

		return commandResponse;
	}

	[HttpGet("{id}/GetRolesAndStatuses")]
	[Authorize(AuthenticationSchemes = "Bearer")]
	[ProducesResponseType(typeof(CollectionResponse<UserRoleStatusDto>), (int)HttpStatusCode.OK)]
	public async Task<CollectionResponse<UserRoleStatusDto>> GetRolesAndStatusesByUserId([FromRoute] string id)
		=> await Mediator.Send(new GetRolesAndStatusesByUserIdQuery() { Id = id });

	[HttpGet("{id}/GetRolesAndDates")]
	[Authorize(AuthenticationSchemes = "Bearer")]
	[ProducesResponseType(typeof(CollectionResponse<UserRoleDatesDto>), (int)HttpStatusCode.OK)]
	public async Task<CollectionResponse<UserRoleDatesDto>> GetRolesAndDatesByUserId([FromRoute] string id)
		=> await Mediator.Send(new GetRolesAndDatesByUserIdQuery() { Id = id });

	[HttpGet("GetUsersByRole/{role}")]
	[Authorize(AuthenticationSchemes = "Bearer")]
	[ProducesResponseType(typeof(ApplicationUserDto), (int)HttpStatusCode.OK)]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
	public async Task<CollectionResponse<ApplicationUserDto>> GetLoggedInUserRole([FromRoute] string role)
		=> await Mediator.Send(new GetUsersByRoleQuery() { Role = role });
}
