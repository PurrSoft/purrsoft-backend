using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews;
using PurrSoft.Application.QueryOverviews.Mappers;
using PurrSoft.Common.Helpful;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Queries.RequestQueries;

public class RequestQueryHandler(IRepository<Request> requestRepository, ILogRepository<Request> logRepository) :
	IRequestHandler<GetFilteredRequestsQueries, CollectionResponse<RequestOverview>>,
	IRequestHandler<GetRequestQuery, RequestDto?>
{
	public async Task<CollectionResponse<RequestOverview>> Handle(GetFilteredRequestsQueries request, CancellationToken cancellationToken)
	{
		IQueryable<Request> query = requestRepository.Query();

		request.LowerCreatedAt = request.LowerCreatedAt != null ?
			DateTime.SpecifyKind(request.LowerCreatedAt.Value, DateTimeKind.Utc) : null;
		request.UpperCreatedAt = request.UpperCreatedAt != null ?
			DateTime.SpecifyKind(request.UpperCreatedAt.Value, DateTimeKind.Utc) : null;

		try
		{
			query = query.ApplyFilter(request);
		}
		catch (ArgumentException ex)
		{
			logRepository.Log(LogLevel.Error, ex.Message);
			return new CollectionResponse<RequestOverview>([], 0);
		}

		IQueryable<RequestOverview> requestOverviews = query.ProjectToOverview();
		requestOverviews = requestOverviews
			.SortAndPaginate(request.SortBy, request.SortOrder, request.Skip, request.Take);
		List<RequestOverview> requestOverviewsList = await requestOverviews.ToListAsync(cancellationToken);
		return new CollectionResponse<RequestOverview>(requestOverviewsList, requestOverviewsList.Count);
	}

	public async Task<RequestDto?> Handle(GetRequestQuery request, CancellationToken cancellationToken)
	{
		RequestDto? requestDto = await requestRepository.Query(s => s.Id == request.Id)
			.ProjectToDto()
			.FirstOrDefaultAsync(cancellationToken);

		return requestDto;
	}
}