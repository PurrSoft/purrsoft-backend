using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrSoft.Application.QueryResponses;

public class ShiftCountByDateResponse
{
	public int TotalShiftCount { get; set; }
	public int DayShiftsCount { get; set; }
	public int NightShiftsCount { get; set; }
}
