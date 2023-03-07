using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelTask = Entities.Models.Task;

namespace Repository
{
	public class TaskRepository : RepositoryBase<ModelTask>, ITaskRepository
	{
		public TaskRepository(CodeCompetitionContext context)
			:base(context) 
		{

		}

		public void AddTask(ModelTask task)
		{
			Create(task);
		}

		public void DeleteTask(ModelTask task)
		{
			Delete(task);
		}

		public void UpdateTask(ModelTask task)
		{
			Update(task);
		}

		public async Task<ModelTask> GetTaskById(int taskId)
		{

			return await FindByCondition(t => t.Id == taskId).FirstOrDefaultAsync();
		}

		public async Task<ModelTask> GetTaskByName(string taskName)
		{
			return await FindByCondition(t => t.TaskName== taskName).FirstOrDefaultAsync();		
		}

		public async Task<IEnumerable<ModelTask>> GetTasks()
		{
			return await FindAll().ToListAsync();
		}

		public async Task<IEnumerable<ModelTask>> GetTasksByCompetitionId(int competitionId)
		{
            return await FindAll().Include(t => t.TaskToCompetitions).Where(t => t.TaskToCompetitions.Any(tc => tc.CompetitionId.Equals(competitionId))).ToListAsync();		
		}

		public async Task<IEnumerable<ModelTask>> GetTasksByTaskCategory(int taskCategoryId)
		{
			return await FindAll()
						.Include(t => t.TaskCategory)
						.Where(t => t.TaskCategory.Id.Equals(taskCategoryId))
						.ToListAsync();	
		}

		public async Task<IEnumerable<ModelTask>> GetTasksInProgressByTeamId(int teamId)
		{
			return await FindAll()
						.Include(t => t.TaskCategory)
						.Include(t => t.TaskToTeams)
						.ThenInclude(tt => tt.ParticipantIdForTaskNavigation)
						.Where(t => t.TaskToTeams.Any(tt => tt.TeamId.Equals(teamId) && tt.TaskStatusId == (int)Enums.TaskStatus.InProgress))
						.ToListAsync();	
		}

		public async Task<IEnumerable<ModelTask>> GetTasksSubmittedByCompetitionIdAndTeamId(int competitionId, int teamId)
		{
			return await FindAll()
						.Include(t => t.TaskCategory)
						.Include(t => t.TaskToTeams)
						.ThenInclude(tt => tt.ParticipantIdForTaskNavigation)
						.Include(t => t.TaskToCompetitions)
						.Where(t => t.TaskToTeams.Any(tt => tt.TeamId.Equals(teamId) && tt.TaskStatusId != (int)Enums.TaskStatus.InProgress) 
								&& t.TaskToCompetitions.Any(tt => tt.CompetitionId.Equals(competitionId)))
						.ToListAsync();

		}

		public async Task<IEnumerable<ModelTask>> GetTasksSubmittedByTeamId(int teamId)
		{
			return await FindAll()
						.Include(t => t.TaskCategory)
						.Include(t => t.TaskToTeams)
						.ThenInclude(tt => tt.ParticipantIdForTaskNavigation)
						.Where(t => t.TaskToTeams.Any(tt => tt.TeamId.Equals(teamId) && tt.TaskStatusId != (int)Enums.TaskStatus.InProgress))
						.ToListAsync();
		}

		public async Task<IEnumerable<ModelTask>> GetTasksWithCategory()
		{
			return await FindAll().Include(t => t.TaskCategory).ToListAsync();
		}

		public async Task<bool> IsTaskExist(int taskId)
		{
			return await FindAll().AnyAsync(t => t.Id.Equals(taskId));
		}

		public async Task<bool> IsTaskExist(string taskName)
		{
			return await FindAll().AnyAsync(t => t.TaskName.Equals(taskName));
		}
	}
}
