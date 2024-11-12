using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Entities.Enums;

namespace PurrSoft.Application.Queries.FosterQueries;

public static class FosterFilters
{
    public static IQueryable<Foster> ApplyFilter(
        this IQueryable<Foster> fostersQuery,
        GetFilteredFostersQueries query)
    {
        if (!string.IsNullOrEmpty(query.Search))
        {
            fostersQuery = fostersQuery.Where(f =>
                                f.User.FirstName.Contains(query.Search) ||
                                f.User.LastName.Contains(query.Search) ||
                                f.User.Email.Contains(query.Search));
        }
        if (!string.IsNullOrEmpty(query.Status))
        {
            fostersQuery = fostersQuery.Where(f => f.Status.Equals(Enum.Parse<FosterStatus>(query.Status)));
        }
        if (!string.IsNullOrEmpty(query.Location))
        {
            fostersQuery = fostersQuery.Where(f => f.Location.Contains(query.Location));
        }
        if (!string.IsNullOrEmpty(query.ExperienceLevel))
        {
            fostersQuery = fostersQuery.Where(f => f.ExperienceLevel == query.ExperienceLevel);
        }
        if (query.HasOtherAnimals != null)
        {
            fostersQuery = fostersQuery.Where(f => f.HasOtherAnimals == query.HasOtherAnimals);
        }
        if (query.AnimalFosteredCount != null)
        {
            fostersQuery = fostersQuery.Where(f => f.AnimalFosteredCount == query.AnimalFosteredCount);
        }
        return fostersQuery;
    }
}
