using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Application.Commands.AuthCommands;
using PurrSoft.Application.Common;
using System.Net;
using PurrSoft.Api.Controllers.Base;

namespace PurrSoft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
	public AuthController()
	{
	}

	[AllowAnonymous]
	[HttpPost("Register")]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> Register(UserRegistrationCommand userRegistrationCommand)
	{
		try
		{
			CommandResponse commandResponse = await Mediator.Send(userRegistrationCommand);
			return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
		}
		catch (FluentValidation.ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));

		}
	}

	[HttpPost("Login")]
	[ProducesResponseType(typeof(CommandResponse<UserLoginCommandResponse>), (int)HttpStatusCode.OK)]
	[ProducesResponseType(typeof(CommandResponse), (int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> Login(UserLoginCommand userLoginCommand)
	{
		try
		{
			CommandResponse<UserLoginCommandResponse> commandResponse = await Mediator.Send(userLoginCommand);
			return commandResponse.IsValid ? Ok(commandResponse) : BadRequest(commandResponse);
		}
		catch (FluentValidation.ValidationException ex)
		{
			return BadRequest(new CommandResponse(ex.Errors.ToList()));
		}
	}


	private void SetTokenCookie(string token)
	{
		CookieOptions cookieOptions = new CookieOptions
		{
			HttpOnly = false,
			Secure = false,
			SameSite = SameSiteMode.Lax,
			Expires = DateTime.UtcNow.AddHours(1)
		};
		Response.Cookies.Append("auth_token", token, cookieOptions);
	}

	[HttpPost("Logout")]
	public async Task<IActionResult> Logout()
	{
		Response.Cookies.Append("auth_token", "", new CookieOptions
		{
			Expires = DateTime.UtcNow.AddDays(-1),
			HttpOnly = true,
			Secure = false,
			SameSite = SameSiteMode.Lax
		});

		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		return Ok(new { message = "Logout successful" });
	}
}
