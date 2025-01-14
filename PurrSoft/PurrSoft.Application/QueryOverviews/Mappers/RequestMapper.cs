using PurrSoft.Application.Models;
using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.QueryOverviews.Mappers;

public static class RequestMapper
{
	public static IQueryable<RequestOverview> ProjectToOverview(this IQueryable<Request> query)
		=> query.Select(r => new RequestOverview
		{
			Id = r.Id,
			CreatedAt = r.CreatedAt,
			UserId = r.UserId,
			RequestType = r.RequestType.ToString(),
			Description = r.Description
		});
}
