using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrSoft.Application.QueryOverviews;

public class AnimalFosterMapOverview
{
	public Guid Id { get; set; }
	public Guid AnimalId { get; set; }
	public string FosterId { get; set; }
}
