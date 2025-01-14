using PurrSoft.Application.Models;
using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.QueryOverviews.Mappers;

public static class ShiftMapper
{
	public static IQueryable<ShiftDto> ProjectToDto(this IQueryable<Shift> query)
		=> query.Select(s => new ShiftDto
		{
			Id = s.Id,
			Start = s.Start,
			ShiftType = s.ShiftType.ToString(),
			ShiftStatus = s.ShiftStatus.ToString(),
			VolunteerId = s.VolunteerId
		});

	public static IQueryable<ShiftOverview> ProjectToOverview(this IQueryable<Shift> query)
		=> query.Select(s => new ShiftOverview
		{
			Id = s.Id,
			Start = s.Start,
			ShiftType = s.ShiftType.ToString(),
			ShiftStatus = s.ShiftStatus.ToString(),
			VolunteerId = s.VolunteerId
		});

    public static IQueryable<ShiftVolunteerDto> ProjectToShiftVolunteerDto(this IQueryable<Shift> query)
        => query.Select(s => new ShiftVolunteerDto
        {
            ShiftId = s.Id.ToString(),
            VolunteerId = s.VolunteerId,
            ShiftType = s.ShiftType.ToString(),
            FullName = s.Volunteer.User.FullName,
            Email = s.Volunteer.User.Email
        });
}
