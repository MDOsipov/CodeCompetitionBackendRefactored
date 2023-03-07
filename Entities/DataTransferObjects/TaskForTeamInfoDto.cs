using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class TaskForTeamInfoDto
	{
		public int Id { get; set; }
		public string TaskName { get; set; } = string.Empty;
		public string TaskDescription { get; set; } = string.Empty;
		public string TaskContent { get; set; } = string.Empty;
		public TimeSpan Timeframe { get; set; }
		public int Points { get; set; }
		public string? TaskCategoryName { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public string? GithubUrl { get; set; }
		public string? SubmitterComment { get; set; }
		public string? CompetitionAdminComment { get; set; }
		public int? ParticipantIdForTask { get; set; }
		public string? ParticipantEmailForTask { get; set; }
		public int? TaskStatusId { get; set; }
		public int? ReachedScore { get; set; }
	}
}
