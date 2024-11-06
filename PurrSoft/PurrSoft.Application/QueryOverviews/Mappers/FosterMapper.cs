using PurrSoft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrSoft.Application.QueryOverviews.Mappers
{
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
				Status = f.Status,
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
	}
}
