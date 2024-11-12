using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrSoft.Domain.Entities.Enums
{
	public enum FosterStatus
	{
		Active = 0, // currently fostering
		Available = 1, // currently fostering, but available for fostering
		Inactive = 2, // not currently fostering, but available for fostering
		OnHold = 3, // temporarily not available for fostering
	}
}
