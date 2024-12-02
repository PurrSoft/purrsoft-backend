using PurrSoft.Application.Queries.FosterQueries;
using PurrSoft.Domain.Entities.Enums;
using PurrSoft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrSoft.Application.Queries.AnimalFosterMapQueries;

public static class AnimalFosterMapFilters
{
	public static IQueryable<AnimalFosterMap> ApplyFilter(
		this IQueryable<AnimalFosterMap> animalFosterMapsQuery,
		GetFilteredAnimalFosterMapsQueries query)
	{
		if (!string.IsNullOrEmpty(query.SupervisingComment))
		{
			animalFosterMapsQuery = animalFosterMapsQuery.Where(
					a => a.SupervisingComment.Contains(query.SupervisingComment));
		}
		if (query.StartFosteringDate != null)
		{
			animalFosterMapsQuery = animalFosterMapsQuery.Where(
									a => a.StartFosteringDate >= query.StartFosteringDate);
		}
		if (query.EndFosteringDate != null)
		{
			animalFosterMapsQuery = animalFosterMapsQuery.Where(
													a => a.EndFosteringDate <= query.EndFosteringDate);
		}
		if(!string.IsNullOrEmpty(query.FosterId))
		{
			animalFosterMapsQuery = animalFosterMapsQuery.Where(
								a => a.FosterId.ToString() == query.FosterId);
		}
		if (!string.IsNullOrEmpty(query.AnimalId))
		{
			animalFosterMapsQuery = animalFosterMapsQuery.Where(
												a => a.AnimalId.ToString() == query.AnimalId);
		}
		return animalFosterMapsQuery;
	}
}
