using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Common.Identity;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Entities.Enum;
using PurrSoft.Domain.Repositories;

namespace AlbumStore.Application.Commands.VolunteerCommands;

public class VolunteerCommandHandler(
    IRepository<Volunteer> _volunteerRepository,
    ILogRepository<VolunteerCommandHandler> _logRepository,
    IRepository<ApplicationUser> _userRepository,
    ICurrentUserService _currentUserService) :
    IRequestHandler<CreateVolunteerCommand, CommandResponse>,
    IRequestHandler<UpdateVolunteerCommand, CommandResponse>,
    IRequestHandler<DeleteVolunteerCommand, CommandResponse>
{
    public async Task<CommandResponse> Handle(CreateVolunteerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Volunteer volunteer = new Volunteer
            {
                UserId = request.VolunteerDto.UserId,
                StartDate = DateTime.SpecifyKind(DateTime.Parse(request.VolunteerDto.StartDate), 
                                                DateTimeKind.Utc),
                EndDate = null,
                Status = Enum.Parse<VolunteerStatus>(request.VolunteerDto.Status),
                Tier = Enum.Parse<TierLevel>(request.VolunteerDto.Tier),
                AssignedArea = request.VolunteerDto.AssignedArea,
                LastShiftDate = null,
                ProfilePictureUrl = request.VolunteerDto.ProfilePictureUrl,
                Bio = request.VolunteerDto.Bio,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _volunteerRepository.Add(volunteer);
            await _volunteerRepository.SaveChangesAsync(cancellationToken);

            return CommandResponse.Ok();
        }
        catch (Exception ex)
        {
            _logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }

    public async Task<CommandResponse> Handle(UpdateVolunteerCommand request, CancellationToken cancellationToken)
    {
        CurrentUser currentUser = await _currentUserService.GetCurrentUser();

        if (currentUser == null)
        {
            throw new UnauthorizedAccessException();
        }

        ApplicationUser user = await _userRepository
            .Query(u => u.Id == currentUser.UserId)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(cancellationToken);

        List<string> userRoles = user.UserRoles
            .Select(ur => ur.Role.Name).ToList();

        if (userRoles != null &&
            userRoles.Contains("Volunteer") &&
            currentUser.UserId != request.VolunteerDto.UserId)
        {
            throw new UnauthorizedAccessException();
        }

        try
        {
            Volunteer volunteer = _volunteerRepository
                .Query(v => v.UserId == request.VolunteerDto.UserId).FirstOrDefault();
            if (volunteer == null)
            {
                return CommandResponse.Failed(new[] { "There is no Volunteer with that Id!" });
            }

            volunteer.UserId = request.VolunteerDto.UserId;
            volunteer.StartDate = DateTime.SpecifyKind(DateTime.Parse(request.VolunteerDto.StartDate), 
                                                        DateTimeKind.Utc);
            volunteer.EndDate = request.VolunteerDto.EndDate != null ? 
                                DateTime.SpecifyKind(DateTime.Parse(request.VolunteerDto.EndDate), 
                                                    DateTimeKind.Utc) : null;
            volunteer.Status = Enum.Parse<VolunteerStatus>(request.VolunteerDto.Status);
            volunteer.Tier = Enum.Parse<TierLevel>(request.VolunteerDto.Tier);
            volunteer.AssignedArea = request.VolunteerDto.AssignedArea;
            volunteer.ProfilePictureUrl = request.VolunteerDto.ProfilePictureUrl;
            volunteer.Bio = request.VolunteerDto.Bio;
            volunteer.UpdatedAt = DateTime.UtcNow;

            await _volunteerRepository.SaveChangesAsync(cancellationToken);

            return CommandResponse.Ok();
        }
        catch (Exception ex)
        {
            _logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }

    public async Task<CommandResponse> Handle(DeleteVolunteerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Volunteer volunteer = await _volunteerRepository
                .Query(v => v.UserId == request.Id).FirstOrDefaultAsync();
            if (volunteer == null)
            {
                return CommandResponse.Failed(new[] { "There is no Volunteer with that Id!" });
            }

            _volunteerRepository.Remove(volunteer);
            await _volunteerRepository.SaveChangesAsync(cancellationToken);

            return CommandResponse.Ok();
        }
        catch (Exception ex)
        {
            _logRepository.LogException(LogLevel.Error, ex);
            throw;
        }
    }
}
