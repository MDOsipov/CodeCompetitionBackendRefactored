using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<CompetitionStatus> CompetitionStatuses { get; } = new List<CompetitionStatus>();

    public virtual ICollection<Competition> Competitions { get; } = new List<Competition>();

    public virtual ICollection<Participant> Participants { get; } = new List<Participant>();

    public virtual ICollection<Role> Roles { get; } = new List<Role>();

    public virtual ICollection<TaskCategory> TaskCategories { get; } = new List<TaskCategory>();

    public virtual ICollection<TaskStatus> TaskStatuses { get; } = new List<TaskStatus>();

    public virtual ICollection<TaskToCompetition> TaskToCompetitions { get; } = new List<TaskToCompetition>();

    public virtual ICollection<TaskToTeam> TaskToTeams { get; } = new List<TaskToTeam>();

    public virtual ICollection<Task> Tasks { get; } = new List<Task>();

    public virtual ICollection<Team> Teams { get; } = new List<Team>();

    public virtual ICollection<UserToRole> UserToRoles { get; } = new List<UserToRole>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
