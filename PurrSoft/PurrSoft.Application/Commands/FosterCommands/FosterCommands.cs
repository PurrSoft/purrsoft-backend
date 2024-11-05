using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrSoft.Application.Commands.FosterCommands
{
	public class FosterCommands
	{
		public class CreateFosterCommand : BaseRequest<CommandResponse>
		{
			public FosterDto FosterDto { get; set; }
		}

		public class UpdateFosterCommand : BaseRequest<CommandResponse>
		{
			public FosterDto FosterDto { get; set; }
		}

		public class DeleteFosterCommand : BaseRequest<CommandResponse>
		{
			public string Id { get; set; }
		}
	}
}
