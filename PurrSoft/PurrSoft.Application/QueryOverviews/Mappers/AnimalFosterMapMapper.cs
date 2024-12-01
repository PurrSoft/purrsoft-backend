using PurrSoft.Application.Models;
using PurrSoft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrSoft.Application.QueryOverviews.Mappers;

public static class AnimalFosterMapMapper
{
	public static IQueryable<AnimalFosterMapDto> ProjectToDto(this IQueryable<AnimalFosterMap> query)
		=> query.Select(f => new AnimalFosterMapDto
		{
			Id = f.Id,
			AnimalId = f.AnimalId,
			FosterId = f.FosterId.ToString(),
			StartFosteringDate = f.StartFosteringDate,
			EndFosteringDate = f.EndFosteringDate,
			SupervisingComment = f.SupervisingComment
		});

	public static IQueryable<AnimalFosterMapOverview> ProjectToOverview(this IQueryable<AnimalFosterMap> query)
		=> query.Select(f => new AnimalFosterMapOverview
		{
			Id = f.Id,
			AnimalId = f.AnimalId,
			FosterId = f.FosterId.ToString()
		});
}
