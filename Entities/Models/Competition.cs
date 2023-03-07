using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Competition
{
    public int Id { get; set; }

    public string CompetitionName { get; set; } = null!;

    public int MaxTasksPerGroups { get; set; }

    public int CompetitionStatusId { get; set; }

    public string? HashCode { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public int StatusId { get; set; }

    public string? CompetitionAdministratorId { get; set; }

    public virtual CompetitionStatus CompetitionStatus { get; set; } = null!;

    public virtual User CreateUser { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<TaskToCompetition> TaskToCompetitions { get; } = new List<TaskToCompetition>();

    public virtual User UpdateUser { get; set; } = null!;
}
