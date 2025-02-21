﻿using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.Queries.VolunteerQueries;

public static class VolunteerFilters
{
    public static IQueryable<Volunteer> ApplyFilter(
        this IQueryable<Volunteer> volunteersQuery,
        GetFilteredVolunteersQueries query)
    {
        if (!string.IsNullOrEmpty(query.Search))
        {
            volunteersQuery = volunteersQuery
                .Where(v => v.User.FirstName.Contains(query.Search) ||
                        v.User.LastName.Contains(query.Search) ||
                        v.User.Email.Contains(query.Search));
        }
        if (!string.IsNullOrEmpty(query.Status))
        {
            volunteersQuery = volunteersQuery
                .Where(v => v.Status.ToString() == query.Status);
        }
        if (!string.IsNullOrEmpty(query.Tier))
        {
            volunteersQuery = volunteersQuery
                .Where(v => v.Tier.ToString() == query.Tier);
        }

        if (query.LowerStartDate != null)
        {
			volunteersQuery = volunteersQuery
				.Where(v => v.StartDate >= query.LowerStartDate);
		}

		if (query.UpperStartDate != null)
		{
			volunteersQuery = volunteersQuery
				.Where(v => v.StartDate <= query.UpperStartDate);
		}

		if (query.LowerEndDate != null)
		{
            volunteersQuery = volunteersQuery
                .Where(v => v.EndDate >= query.LowerEndDate);
		}

		if (query.UpperEndDate != null)
		{
            volunteersQuery = volunteersQuery
                .Where(v => v.EndDate <= query.UpperEndDate);
		}

		return volunteersQuery;
    }
}

