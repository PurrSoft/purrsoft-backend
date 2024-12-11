using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.Queries.AnimalQueries;

public static class AnimalFilters
{
    public static IQueryable<Animal> ApplyFilter(
        this IQueryable<Animal> animalsQuery,
        GetFilteredAnimalsQueries query)
    {
        if (!string.IsNullOrEmpty(query.Search))
        {
            animalsQuery = animalsQuery
                .Where(a => a.Name.Contains(query.Search));
        }
        if (!string.IsNullOrEmpty(query.AnimalType))
        {
            animalsQuery = animalsQuery
                .Where(a => a.AnimalType.ToString().Equals(query.AnimalType));
        }
        if (query.YearOfBirth != null)
        {
            animalsQuery = animalsQuery
                .Where(a => a.YearOfBirth == query.YearOfBirth);
        }

        return animalsQuery;
    }
}
