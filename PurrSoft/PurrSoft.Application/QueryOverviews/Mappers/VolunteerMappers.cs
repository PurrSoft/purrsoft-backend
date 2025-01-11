using PurrSoft.Application.Models;
using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.QueryOverviews.Mappers;

public static class VolunteerMappers
{
    public static IQueryable<VolunteerOverview> ProjectToOverview(this IQueryable<Volunteer> query)
        => query.Select(v => new VolunteerOverview
        {
            UserId = v.UserId,
            FirstName = v.User.FirstName,
            LastName = v.User.LastName,
            Email = v.User.Email ?? "",
            Status = v.Status.ToString(),
            Tier = v.Tier.ToString()
        });

    public static IQueryable<VolunteerDto> ProjectToDto(this IQueryable<Volunteer> query)
        => query.Select(v => new VolunteerDto
        {
            UserId = v.UserId,
            StartDate = v.StartDate.ToString(),
            EndDate = v.EndDate.ToString(),
            Status = v.Status.ToString(),
            Tier = v.Tier.ToString(),
            LastShiftDate = v.LastShiftDate.ToString(),
            CreatedAt = v.CreatedAt.ToString(),
            UpdatedAt = v.UpdatedAt.ToString(),
            Tasks = v.Tasks,
            AvailableHours = v.AvailableHours,
            TrainingStartDate = v.TrainingStartDate.ToString(),
            SupervisorId = v.Supervisor != null ? v.Supervisor.Id : null,
            TrainersId = v.Trainers != null ? v.Trainers.Select(t => t.Id).ToList() : null
        });
}

