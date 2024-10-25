using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.Commands.AuthCommands;

// user registration
public class UserRegistrationCommand : BaseRequest<CommandResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
}

//user login
public class UserLoginCommand : BaseRequest<CommandResponse<UserLoginCommandResponse>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

//user login response
public class UserLoginCommandResponse
{
    public string Token { get; set; }
    public ApplicationUserDto User { get; set; }
}