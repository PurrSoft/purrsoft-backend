using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Entities.Enums;

namespace PurrSoft.Application.Queries.ShiftQueries;

public static class ShiftFilters
{
	public static IQueryable<Shift> ApplyFilter(
		this IQueryable<Shift> shiftQuery,
		GetFilteredShiftsQueries query)
	{
		if (!string.IsNullOrEmpty(query.ShiftType))
		{
			if (Enum.TryParse(query.ShiftType, out ShiftType shiftType))
			{
				shiftQuery = shiftQuery.Where(s => s.ShiftType == shiftType);
			} else
			{
				throw new ArgumentException("Invalid shift type");
			}
		}

		if (!string.IsNullOrEmpty(query.ShiftStatus))
		{
			if (Enum.TryParse(query.ShiftStatus, out ShiftStatus shiftStatus))
			{
				shiftQuery = shiftQuery.Where(s => s.ShiftStatus == shiftStatus);
			}
			else
			{
				throw new ArgumentException("Invalid shift status");
			}
		}

		if (query.UpperStartTime != null)
		{
			shiftQuery = shiftQuery.Where(s => s.Start <= query.UpperStartTime);
		}

		if (query.LowerStartTime != null)
		{
			shiftQuery = shiftQuery.Where(s => s.Start >= query.LowerStartTime);
		}

		if (!string.IsNullOrEmpty(query.VolunteerId))
		{
			shiftQuery = shiftQuery.Where(s => s.VolunteerId == query.VolunteerId);
		}
		return shiftQuery;
	}

    public static IQueryable<Shift> ApplyDateFilter(
        this IQueryable<Shift> shiftQuery,
        GetShiftVolunteersQuery query)
    {
        return shiftQuery.Where(s => s.Start.Date == query.DayOfShift.Date);
    }
}
