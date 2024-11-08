﻿using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews;

namespace PurrSoft.Application.Queries.FosterQueries;

public class GetFilteredFostersQueries : BaseRequest<CollectionResponse<FosterOverview>>
{
	public int Skip { get; set; }
	public int Take { get; set; }
	public string? SortBy { get; set; }
	public string? SortOrder { get; set; }
	public string? Search { get; set; }
	public string? Status { get; set; }
	public string? Location { get; set; }
	public string? ExperienceLevel { get; set; }
	public bool? HasOtherAnimals { get; set; }
	public int? AnimalFosteredCount { get; set; }
}

public class GetFosterByIdQuery : BaseRequest<FosterDto>
{
	public required string Id { get; set; }
}
