using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Models;
using Task = System.Threading.Tasks.Task;
using ModelTask = Entities.Models.Task;

namespace Contracts
{
	public interface ITaskRepository
	{
		Task<IEnumerable<ModelTask>> GetTasks();
		Task<IEnumerable<ModelTask>> GetTasksWithCategory();
		Task<IEnumerable<ModelTask>> GetTasksByCompetitionId(int competitionId);
		Task<ModelTask> GetTaskById(int taskId);
		Task<ModelTask> GetTaskByName(string taskName);
		Task<IEnumerable<ModelTask>> GetTasksByTaskCategory(int taskCategoryId);
		Task<IEnumerable<ModelTask>> GetTasksInProgressByTeamId(int teamId);
		Task<IEnumerable<ModelTask>> GetTasksSubmittedByTeamId(int teamId);
		Task<IEnumerable<ModelTask>> GetTasksSubmittedByCompetitionIdAndTeamId(int competitionId, int teamId);
		void AddTask(ModelTask task);
		void UpdateTask(ModelTask task);
		void DeleteTask(ModelTask task);
		Task<bool> IsTaskExist(int taskId);
		Task<bool> IsTaskExist(string taskName);

	}
}
