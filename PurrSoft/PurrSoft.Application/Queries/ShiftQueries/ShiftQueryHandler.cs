using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews;
using PurrSoft.Application.QueryOverviews.Mappers;
using PurrSoft.Application.QueryResponses;
using PurrSoft.Common.Helpful;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Entities.Enums;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Queries.ShiftQueries;

public class ShiftQueryHandler(IRepository<Shift> shiftRepository, ILogRepository<Shift> logRepository) :
	IRequestHandler<GetFilteredShiftsQueries, CollectionResponse<ShiftOverview>>,
	IRequestHandler<GetShiftQuery, ShiftDto?>,
	IRequestHandler<GetShiftCountQuery, ShiftCountByDateResponse>
{
	public async Task<CollectionResponse<ShiftOverview>> Handle(GetFilteredShiftsQueries request, CancellationToken cancellationToken)
	{
		IQueryable<Shift> query = shiftRepository.Query();

		request.Start = request.Start != null ?
			DateTime.SpecifyKind(request.Start.Value, DateTimeKind.Utc) : null;

		try
		{
			query = query.ApplyFilter(request);
		}
		catch (ArgumentException ex)
		{
			logRepository.Log(LogLevel.Error, ex.Message);
			return new CollectionResponse<ShiftOverview>([], 0);
		}

		IQueryable<ShiftOverview> shiftOverviews = query.ProjectToOverview();
		shiftOverviews = shiftOverviews
			.SortAndPaginate(request.SortBy, request.SortOrder, request.Skip, request.Take);
		List<ShiftOverview> shiftOverviewsList = await shiftOverviews.ToListAsync(cancellationToken);
		return new CollectionResponse<ShiftOverview>(shiftOverviewsList, shiftOverviewsList.Count);
	}

	public async Task<ShiftDto?> Handle(GetShiftQuery request, CancellationToken cancellationToken)
	{
		ShiftDto? shiftDto = await shiftRepository.Query(s => s.Id == request.Id)
			.ProjectToDto()
			.FirstOrDefaultAsync(cancellationToken);

		return shiftDto;
	}

	public async Task<ShiftCountByDateResponse> Handle(GetShiftCountQuery request, CancellationToken cancellationToken)
	{
		var shifts = await shiftRepository.Query(s => s.Start.Date == request.Date.Date).ToListAsync(cancellationToken);	

		ShiftCountByDateResponse shiftCountByDateResponse = new()
		{
			TotalShiftCount = shifts.Count,
			DayShiftsCount = shifts.Count(s => s.ShiftType == ShiftType.Day),
			NightShiftsCount = shifts.Count(s => s.ShiftType == ShiftType.Night)
		};

		return shiftCountByDateResponse;
	}
}
