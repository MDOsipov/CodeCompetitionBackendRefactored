using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class TaskCategory
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? CategoryDescription { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public int StatusId { get; set; }

    public virtual User CreateUser { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; } = new List<Task>();

    public virtual User UpdateUser { get; set; } = null!;
}
