using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class TaskDto
	{
		public int Id { get; set; }

		public string TaskName { get; set; } = null!;

		public string TaskDescription { get; set; } = null!;

		public string TaskContent { get; set; } = null!;

		public int TaskCategoryId { get; set; }

		public TimeSpan Timeframe { get; set; }

		public int Points { get; set; }

	}
}
