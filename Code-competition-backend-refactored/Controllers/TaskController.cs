using Microsoft.AspNetCore.Mvc;
using Repository;
using Contracts;
using Entities.Models;
using Entities.DataTransferObjects;
using ModelTask = Entities.Models.Task;

using AutoMapper;
using Entities;

namespace Code_competition_backend_refactored.Controllers
{
	[Route("[controller]")]
	[ApiController]

	public class TaskController : ControllerBase
	{
		private readonly IRepositoryWrapper _repository;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;

		public TaskController(IRepositoryWrapper wrapper, IMapper mapper, ILoggerManager logger)
		{
			_repository = wrapper;
			_mapper = mapper;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetTasks()
		{
			_logger.LogDebug("GetTasks method started");

			try
			{
				var tasks = await _repository.Task.GetTasks();

				var tasksResult = _mapper.Map<IEnumerable<TaskDto>>(tasks);

				_logger.LogInfo("Success. Tasks are received");
				return Ok(tasksResult);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetTasks action: {ex.Message}");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("byTaskId/{taskId}", Name = "TaskById")]
		public async Task<IActionResult> GetTaskById(int taskId)
		{
			_logger.LogDebug($"GetTaskById method started with taskId: {taskId}");

			if (!await _repository.Task.IsTaskExist(taskId))
			{
				_logger.LogInfo($"Not found. No task with task id of {taskId}");
				return NotFound();
			}

			try
			{
				var task = await _repository.Task.GetTaskById(taskId);
				var taskResult = _mapper.Map<TaskDto>(task);

				_logger.LogInfo("Success. Task is received");
				return Ok(taskResult);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetTaskById action: {ex.Message}");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("tasksWithCategories")]
		public async Task<IActionResult> GetTasksWithCategories()
		{
			_logger.LogDebug("GetTasksWithCategories method started");

			try
			{
				var tasks = await _repository.Task.GetTasksWithCategory();

				var tasksResult = _mapper.Map<IEnumerable<TaskWithCategoryInfoDto>>(tasks);

				_logger.LogInfo("Success. Tasks are received");
				return Ok(tasksResult);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetTasksWithCategories action: {ex.Message}");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("byCompetitionId/{competitionId}")]
		public async Task<IActionResult> GetTasksByCompetitionId(int competitionId)
		{
			_logger.LogDebug($"GetTasksByCompetitionId method started with competitionId: {competitionId}");

			try
			{
				var tasks = await _repository.Task.GetTasksByCompetitionId(competitionId);

				if (tasks.Count() == 0)
				{
					_logger.LogInfo($"Not found. No tasks with competition id of {competitionId}");
					return NotFound();
				}

				var tasksResult = _mapper.Map<IEnumerable<TaskDto>>(tasks);

				_logger.LogInfo("Success. Tasks are received");
				return Ok(tasksResult);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetTasksByCompetitionId action: {ex.Message}");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("getTasksInProgressByTeamId/{teamId}")]
		public async Task<IActionResult> GetTasksInProgressByTeamId(int teamId)
		{
			_logger.LogDebug($"GetTasksInProgressByTeamId method started with team id: {teamId}");

			try
			{
				var tasks = await _repository.Task.GetTasksInProgressByTeamId(teamId);

				if (tasks.Count() == 0)
				{
					return NotFound();
				}

				var tasksResult = _mapper.Map<IEnumerable<TaskForTeamInfoDto>>(tasks);

				_logger.LogInfo("Success. Tasks are received");
				return Ok(tasksResult);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetTasksInProgressByTeamId action: {ex.Message}");
				return StatusCode(500, ex.Message);
			}
		}


		[HttpGet("submitted/byTeamId/{teamId}")]
		public async Task<IActionResult> GetTasksSubmittedByTeamId(int teamId)
		{
			_logger.LogDebug($"GetTasksSubmittedByTeamId method started with team id: {teamId}");

			try
			{
				var tasks = await _repository.Task.GetTasksSubmittedByTeamId(teamId);

				if (tasks.Count() == 0)
				{
					return NotFound();
				}

				var tasksResult = _mapper.Map<IEnumerable<TaskForTeamInfoDto>>(tasks);

				_logger.LogInfo("Success. Tasks are received");
				return Ok(tasksResult);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetTasksSubmittedByTeamId action: {ex.Message}");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("submitted/all/{competitionId}/{teamId}")]
		public async Task<IActionResult> GetAllSubmittedTasksByCompetitionIdAndTeamId(int competitionId, int teamId)
		{
			_logger.LogDebug($"GetAllSubmittedTasksByCompetitionIdAndTeamId method started with competition id: {competitionId} and team id: {teamId}");

			try
			{
				var tasks = await _repository.Task.GetTasksSubmittedByCompetitionIdAndTeamId(competitionId, teamId);

				if (tasks.Count() == 0)
				{
					return NotFound();
				}

				var tasksResult = _mapper.Map<IEnumerable<TaskForTeamInfoDto>>(tasks);

				_logger.LogInfo("Success. Tasks are received");
				return Ok(tasksResult);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetTasksSubmittedByTeamId action: {ex.Message}");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{taskCategoryId}/Tasks")]
		public async Task<IActionResult> GetTaskByTaskCategory(int taskCategoryId)
		{
			_logger.LogDebug($"GetTaskByTaskCategory method started with task category id: {taskCategoryId}");
			try
			{
				var tasks = await _repository.Task.GetTasksByTaskCategory(taskCategoryId);

				if (tasks.Count() == 0)
				{
					return NotFound();
				}

				var tasksResult = _mapper.Map<IEnumerable<TaskWithCategoryInfoDto>>(tasks);

				_logger.LogInfo("Success. Tasks are received");
				return Ok(tasksResult);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetTasksInProgressByTeamId action: {ex.Message}");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateTask([FromBody] TaskForCreationDto task)
		{
			_logger.LogDebug($"CreateTask method started");
			try
			{
				if (task is null)
				{
					_logger.LogError("Task object sent from client is null");
					return BadRequest("Task object is null");
				}

				if (!ModelState.IsValid)
				{
					_logger.LogError("Task object sent from client is invalid");
					return BadRequest("Task object is invalid");
				}

				var taskEntity = _mapper.Map<ModelTask>(task);

				taskEntity.CreateDate = DateTime.Now;
				taskEntity.UpdateDate = DateTime.Now;
				taskEntity.CreateUserId = 1;
				taskEntity.UpdateUserId = 1;
				taskEntity.StatusId = (int)Enums.Status.Active;

				_repository.Task.AddTask(taskEntity);
				await _repository.Save();

				var createdTask = _mapper.Map<TaskDto>(taskEntity);

				_logger.LogDebug($"Task is successfully created");
				return CreatedAtRoute("TaskById", new { taskId = createdTask.Id }, createdTask);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside CreateTask action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpPut("{taskId}")]
		public async Task<IActionResult> UpdateTask(int taskId, [FromBody] TaskForUpdateDto task)
		{
			_logger.LogDebug($"UpdateTask method started");

			try
			{
				if (task is null)
				{
					_logger.LogError("Task object sent from client is null");
					return BadRequest("Task object is null");
				}

				if (!ModelState.IsValid)
				{
					_logger.LogError("Task object sent from client is invalid");
					return BadRequest("Task object is invalid");
				}

				if (!await _repository.Task.IsTaskExist(taskId))
				{
					_logger.LogError($"Task with task id: {taskId} does not exist");
					return NotFound();
				}

				var taskEntity = await _repository.Task.GetTaskById(taskId);

				_mapper.Map(task, taskEntity);

				taskEntity.UpdateDate = DateTime.Now;
				taskEntity.UpdateUserId = 1;

				_repository.Task.UpdateTask(taskEntity);
				await _repository.Save();

				_logger.LogDebug($"Task is successfully updated");
				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside UpdateTask action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpDelete("{taskId}")]
		public async Task<IActionResult> DeleteTask(int taskId)
		{
			_logger.LogDebug($"DeleteTask method started with taskId of {taskId}");

			try
			{
				if (!await _repository.Task.IsTaskExist(taskId))
				{
					_logger.LogError($"Task with task id: {taskId} does not exist");
					return NotFound();
				}

				var taskToDelete = await _repository.Task.GetTaskById(taskId);

				taskToDelete.StatusId = (int)Enums.Status.NotActive;
				taskToDelete.UpdateDate = DateTime.Now;
				taskToDelete.UpdateUserId = 1;

				_repository.Task.UpdateTask(taskToDelete);
				await _repository.Save();

				_logger.LogDebug($"Task is successfully deleted");
				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside DeleteTask action: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}

}
}
