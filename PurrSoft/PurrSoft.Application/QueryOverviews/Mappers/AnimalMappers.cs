using PurrSoft.Application.Models;
using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.QueryOverviews.Mappers;

public static class AnimalMappers
{
    public static IQueryable<AnimalOverview> ProjectToOverview(this IQueryable<Animal> query)
    {
        return query.Select(x => new AnimalOverview
        {
            Id = x.Id.ToString(),
            Name = x.Name,
            AnimalType = x.AnimalType.ToString(),
            YearOfBirth = x.YearOfBirth == null ? 0 : (int)x.YearOfBirth,
            Gender = x.Gender,
            Sterilized = x.Sterilized == null ? false : (bool) x.Sterilized,
            Passport = x.Passport,
            ImageUrls = x.ImageUrls
        });
    }

    public static IQueryable<AnimalDto> ProjectToDto(this IQueryable<Animal> query)
    {
        return query.Select(x => new AnimalDto
        {
            Id = x.Id.ToString(),
            Name = x.Name,
            AnimalType = x.AnimalType.ToString(),
            YearOfBirth = x.YearOfBirth == null ? 0 : (int)x.YearOfBirth,
            Gender = x.Gender,
            Sterilized = x.Sterilized == null ? false : (bool)x.Sterilized,
            Passport = x.Passport,
            ImageUrls = x.ImageUrls
        });
    }
}
