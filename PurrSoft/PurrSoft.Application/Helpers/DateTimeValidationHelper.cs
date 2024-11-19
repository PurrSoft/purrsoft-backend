﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrSoft.Application.Helpers;

public static class DateTimeValidationHelper
{
	public static bool BeAValidDate(string? date) =>
		DateTime.TryParse(date, out _);

	public static bool BeLaterThanMinValue(string? date) =>
		DateTime.TryParse(date, out var parsedDate) && parsedDate > DateTime.MinValue;

	public static bool BeLaterThan(string? startDate, string? endDate) =>
		DateTime.TryParse(startDate, out var parsedStartDate) &&
		DateTime.TryParse(endDate, out var parsedEndDate) &&
		parsedEndDate > parsedStartDate;
}
