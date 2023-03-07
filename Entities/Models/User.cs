using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public int StatusId { get; set; }

    public virtual ICollection<Competition> CompetitionCreateUsers { get; } = new List<Competition>();

    public virtual ICollection<CompetitionStatus> CompetitionStatusCreateUsers { get; } = new List<CompetitionStatus>();

    public virtual ICollection<CompetitionStatus> CompetitionStatusUpdateUsers { get; } = new List<CompetitionStatus>();

    public virtual ICollection<Competition> CompetitionUpdateUsers { get; } = new List<Competition>();

    public virtual ICollection<Participant> ParticipantCreateUsers { get; } = new List<Participant>();

    public virtual ICollection<Participant> ParticipantUpdateUsers { get; } = new List<Participant>();

    public virtual ICollection<Participant> ParticipantUsers { get; } = new List<Participant>();

    public virtual ICollection<Role> Roles { get; } = new List<Role>();

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<TaskCategory> TaskCategoryCreateUsers { get; } = new List<TaskCategory>();

    public virtual ICollection<TaskCategory> TaskCategoryUpdateUsers { get; } = new List<TaskCategory>();

    public virtual ICollection<Task> TaskCreateUsers { get; } = new List<Task>();

    public virtual ICollection<TaskStatus> TaskStatusCreateUsers { get; } = new List<TaskStatus>();

    public virtual ICollection<TaskStatus> TaskStatusUpdateUsers { get; } = new List<TaskStatus>();

    public virtual ICollection<TaskToCompetition> TaskToCompetitionCreateUsers { get; } = new List<TaskToCompetition>();

    public virtual ICollection<TaskToCompetition> TaskToCompetitionUpdateUsers { get; } = new List<TaskToCompetition>();

    public virtual ICollection<TaskToTeam> TaskToTeamCreateUsers { get; } = new List<TaskToTeam>();

    public virtual ICollection<TaskToTeam> TaskToTeamUpdateUsers { get; } = new List<TaskToTeam>();

    public virtual ICollection<Task> TaskUpdateUsers { get; } = new List<Task>();

    public virtual ICollection<Team> TeamCreateUsers { get; } = new List<Team>();

    public virtual ICollection<Team> TeamUpdateUsers { get; } = new List<Team>();

    public virtual ICollection<UserToRole> UserToRoleCreateUsers { get; } = new List<UserToRole>();

    public virtual ICollection<UserToRole> UserToRoleUpdateUsers { get; } = new List<UserToRole>();

    public virtual ICollection<UserToRole> UserToRoleUsers { get; } = new List<UserToRole>();
}
