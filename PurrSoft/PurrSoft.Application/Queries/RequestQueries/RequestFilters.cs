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

		if (query.RequestType == nameof(RequestType.Leave))
		{
			var leaveRequestQuery = requestQuery.OfType<LeaveRequest>();

			if (query.Approved.HasValue)
			{
				leaveRequestQuery = leaveRequestQuery.Where(lr => lr.Approved == query.Approved.Value);
			}

			if (query.LowerStartDate != null)
			{
				leaveRequestQuery = leaveRequestQuery.Where(lr => lr.StartDate >= query.LowerStartDate);
			}

			if (query.UpperStartDate != null)
			{
				leaveRequestQuery = leaveRequestQuery.Where(lr => lr.StartDate <= query.UpperStartDate);
			}

			if (query.LowerEndDate != null)
			{
				leaveRequestQuery = leaveRequestQuery.Where(lr => lr.EndDate >= query.LowerEndDate);
			}

			if (query.UpperEndDate != null)
			{
				leaveRequestQuery = leaveRequestQuery.Where(lr => lr.EndDate <= query.UpperEndDate);
			}

			if (query.Duration != null)
			{
				leaveRequestQuery = leaveRequestQuery.Where(lr => lr.Duration == query.Duration);
			}

			return leaveRequestQuery; // Return specialized query for LeaveRequest
		}

		return requestQuery;
	}
}