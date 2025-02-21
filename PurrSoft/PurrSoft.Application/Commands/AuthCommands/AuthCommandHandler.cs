﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Application.Interfaces;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews.Mappers;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PurrSoft.Application.Commands.AuthCommands;

public class AuthCommandHandler(
    UserManager<ApplicationUser> userManager,
    IJwtTokenService tokenService,
    IRepository<ApplicationUser> userRepository,
    IRepository<Role> roleRepository,
    IConfiguration configuration,
    ILogRepository<AuthCommandHandler> logRepository)
    : IRequestHandler<UserRegistrationCommand, CommandResponse>,
        IRequestHandler<UserLoginCommand, CommandResponse<UserLoginCommandResponse>>,
        IRequestHandler<UserChangePasswordCommand, CommandResponse>
{
    public async Task<CommandResponse> Handle(UserRegistrationCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (command.Role.Length == 0)
            {
                return CommandResponse.Failed("Role should not be empty.");
            }
            if (!await roleRepository.Query(r => r.Name == command.Role).AnyAsync(cancellationToken: cancellationToken))
            {
                return CommandResponse.Failed($"Role '{command.Role}' does not exist.");
            }

            ApplicationUser applicationUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = command.Email,
                UserName = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                EmailConfirmed = false
            };

            IdentityResult creationResult = await userManager.CreateAsync(applicationUser, command.Password);
            if (!creationResult.Succeeded)
            {
                var errors = creationResult.Errors.Select(e => e.Description).ToArray();
                return CommandResponse.Failed(errors);
            }

            IdentityResult roleResult = await userManager.AddToRoleAsync(applicationUser, command.Role);
            if (!roleResult.Succeeded)
            {
                return CommandResponse.Failed(roleResult.Errors.Select(e => e.Description).ToArray());
            }

            return CommandResponse.Ok();
        }
        catch (Exception ex)
        {
            logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }
    public async Task<CommandResponse<UserLoginCommandResponse>> Handle(UserLoginCommand command, CancellationToken cancellationToken)
    {
        try
        {
            ApplicationUser? applicationUser = await userManager.FindByEmailAsync(command.Email);

            CommandResponse<UserLoginCommandResponse> validationResult = ValidateUser(applicationUser);
            if (validationResult != null)
            {
                return validationResult;
            }

            bool passwordValid = await userManager.CheckPasswordAsync(applicationUser, command.Password);

            if (!passwordValid)
            {
                return CommandResponse.Failed<UserLoginCommandResponse>("Invalid username or Password!");
            }

            return await GetApplicationUserDto(applicationUser, cancellationToken);
        }
        catch (Exception ex)
        {
            logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }

    public async Task<CommandResponse> Handle(UserChangePasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ApplicationUser? applicationUser = await userManager.FindByEmailAsync(request.Email);

            CommandResponse<UserLoginCommandResponse> validationResult = ValidateUser(applicationUser);
            if (validationResult != null)
            {
                return validationResult;
            }

            bool passwordValid = await userManager.CheckPasswordAsync(applicationUser, request.CurrentPassword);

            bool passwordMatch = request.NewPassword == request.ConfirmPassword;

            bool passwordContainsRequestedCharacters = userManager.PasswordValidators.All(v => v.ValidateAsync(userManager, applicationUser, request.NewPassword).Result.Succeeded);

            if (!passwordContainsRequestedCharacters)
            {
                return CommandResponse.Failed<UserLoginCommandResponse>("Password does not meet the requirements!");
            }

            if (!passwordValid)
            {
                return CommandResponse.Failed<UserLoginCommandResponse>("Invalid current Password!");
            }

            if (!passwordMatch)
            {
                return CommandResponse.Failed<UserLoginCommandResponse>("Passwords do not match!");
            }

            applicationUser.PasswordHash = userManager.PasswordHasher.HashPassword(applicationUser, request.NewPassword);

            IdentityResult result = await userManager.UpdateAsync(applicationUser);

            if (!result.Succeeded)
            {
                return CommandResponse.Failed(result.Errors.Select(e => e.Description).ToArray());
            }

            return CommandResponse.Ok();
        }
        catch (Exception ex)
        {
            logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }

    private async Task<CommandResponse<UserLoginCommandResponse>> GetApplicationUserDto(ApplicationUser applicationUser, CancellationToken cancellationToken)
    {
        ApplicationUserDto? applicationUserDto = await userRepository.Query(u => u.Id == applicationUser.Id).ProjectToDto().FirstOrDefaultAsync(cancellationToken);

        if (applicationUserDto == null)
        {
            return CommandResponse.Failed<UserLoginCommandResponse>("User not found.");
        }

        UserLoginCommandResponse response = new UserLoginCommandResponse
        {
            Token = tokenService.CreateToken(applicationUser, applicationUserDto.Roles),
            User = applicationUserDto
        };

        return CommandResponse.Ok(response);
    }

    private CommandResponse<UserLoginCommandResponse> ValidateUser(ApplicationUser? applicationUser)
        => (applicationUser switch
        {
            null => CommandResponse.Failed<UserLoginCommandResponse>(@"Invalid username Or Password",
                "User not found."),
            _ => null
        })!;
}