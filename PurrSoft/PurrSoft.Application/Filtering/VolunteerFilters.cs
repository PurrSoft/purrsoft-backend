using AlbumStore.Application.Queries.VolunteerQueries;
using PurrSoft.Domain.Entities;

namespace AlbumStore.Application.Filtering;

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
        if (!string.IsNullOrEmpty(query.AssignedArea))
        {
            volunteersQuery = volunteersQuery
                .Where(v => v.AssignedArea == query.AssignedArea);
        }

        return volunteersQuery;
    }
}

