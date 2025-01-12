using PurrSoft.Application.Models;
using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.QueryOverviews.Mappers;

public static class RequestMapper
{
	public static IQueryable<RequestDto> ProjectToDto(this IQueryable<Request> query)
		=> query.Select(r => new RequestDto
		{
			Id = r.Id,
			CreatedAt = r.CreatedAt,
			UserId = r.UserId,
			RequestType = r.RequestType.ToString(),
			Description = r.Description,
			Images = r.Images
		});

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
