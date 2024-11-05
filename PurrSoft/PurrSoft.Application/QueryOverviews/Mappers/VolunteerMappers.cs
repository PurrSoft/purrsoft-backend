using PurrSoft.Domain.Entities;

namespace AlbumStore.Application.QueryOverviews.Mappers;

public static class VolunteerMappers
{
    public static IQueryable<VolunteerOverview> ProjectToOverview(this IQueryable<Volunteer> query)
        => query.Select(v => new VolunteerOverview
        {
            UserId = v.UserId,
            FirstName = v.User.FirstName,
            LastName = v.User.LastName,
            Email = v.User.Email,
            StartDate = v.StartDate,
            EndDate = v.EndDate,
            Status = v.Status.ToString(),
            Tier = v.Tier.ToString(),
            AssignedArea = v.AssignedArea,
            LastShiftDate = v.LastShiftDate,
            ProfilePictureUrl = v.ProfilePictureUrl,
            Bio = v.Bio,
            CreatedAt = v.CreatedAt,
            UpdatedAt = v.UpdatedAt
        });
}

