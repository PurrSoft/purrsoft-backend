using PurrSoft.Application.Models;
using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.QueryOverviews.Mappers;

public static class FosterMapper
{
	public static IQueryable<FosterOverview> ProjectToOverview(this IQueryable<Foster> query)
		=> query.Select(f => new FosterOverview
		{
			UserId = f.UserId,
			FirstName = f.User.FirstName,
			LastName = f.User.LastName,
			Email = f.User.Email ?? "",
			StartDate = f.StartDate,
			EndDate = f.EndDate,
			Status = f.Status.ToString(),
			Location = f.Location,
			MaxAnimalsAllowed = f.MaxAnimalsAllowed,
			HomeDescription = f.HomeDescription,
			ExperienceLevel = f.ExperienceLevel,
			HasOtherAnimals = f.HasOtherAnimals,
			OtherAnimalDetails = f.OtherAnimalDetails,
			AnimalFosteredCount = f.AnimalFosteredCount,
			//FosteredAnimals = f.FosteredAnimals,
			CreatedAt = f.CreatedAt,
			UpdatedAt = f.UpdatedAt
		});

	public static IQueryable<FosterDto> ProjectToDto(this IQueryable<Foster> query)
		=> query.Select(f => new FosterDto
		{
			UserId = f.UserId,
			StartDate = f.StartDate,
			EndDate = f.EndDate,
			Status = f.Status.ToString(),
			Location = f.Location,
			MaxAnimalsAllowed = f.MaxAnimalsAllowed,
			HomeDescription = f.HomeDescription,
			ExperienceLevel = f.ExperienceLevel,
			HasOtherAnimals = f.HasOtherAnimals,
			OtherAnimalDetails = f.OtherAnimalDetails,
			AnimalFosteredCount = f.AnimalFosteredCount,
		});
}
