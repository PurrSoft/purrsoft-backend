using PurrSoft.Domain.Entities.Enums;
using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.Queries.RequestQueries;

public static class RequestFilters
{
	public static IQueryable<Request> ApplyFilter(
		this IQueryable<Request> requestQuery,
		GetFilteredRequestsQueries query)
	{
		if (!string.IsNullOrEmpty(query.RequestType))
		{
			if (Enum.TryParse(query.RequestType, out RequestType requestType))
			{
				requestQuery = requestQuery.Where(r => r.RequestType == requestType);
			}
			else
			{
				throw new ArgumentException("Invalid request type");
			}
		}

		if (query.LowerCreatedAt != null)
		{
			requestQuery = requestQuery.Where(r => r.CreatedAt >= query.LowerCreatedAt);
		}

		if (query.UpperCreatedAt != null)
		{
			requestQuery = requestQuery.Where(r => r.CreatedAt <= query.UpperCreatedAt);
		}

		if (!string.IsNullOrEmpty(query.UserId))
		{
			requestQuery = requestQuery.Where(r => r.UserId == query.UserId);
		}

		return requestQuery;
	}
}