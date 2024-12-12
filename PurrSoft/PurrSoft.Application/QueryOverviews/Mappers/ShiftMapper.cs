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
			VolunteerId = s.VolunteerId
		});

	public static IQueryable<ShiftOverview> ProjectToOverview(this IQueryable<Shift> query)
		=> query.Select(s => new ShiftOverview
		{
			Id = s.Id,
			Start = s.Start,
			ShiftType = s.ShiftType.ToString(),
			VolunteerId = s.VolunteerId
		});
}
