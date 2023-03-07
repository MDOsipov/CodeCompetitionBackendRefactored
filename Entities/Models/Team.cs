﻿using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Team
{
    public int Id { get; set; }

    public string TeamName { get; set; } = null!;

    public int TeamLeaderId { get; set; }

    public int TotalPoints { get; set; }

    public string? IconImage { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public int StatusId { get; set; }

    public int? CompetitionId { get; set; }

    public virtual User CreateUser { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<TaskToTeam> TaskToTeams { get; } = new List<TaskToTeam>();

    public virtual User UpdateUser { get; set; } = null!;
}
