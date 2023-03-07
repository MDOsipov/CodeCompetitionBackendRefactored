﻿using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class TaskStatus
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public int StatusId { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public virtual User CreateUser { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<TaskToTeam> TaskToTeams { get; } = new List<TaskToTeam>();

    public virtual User UpdateUser { get; set; } = null!;
}
