using AutoMapper;
using Code_competition_backend_refactored.Controllers;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelTask = Entities.Models.Task;

namespace CodeCompetitionBackend.test.ControllerTests
{
	public class TaskControllerTests
	{
		private readonly IRepositoryWrapper _repository;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly TaskController _taskController;

		public TaskControllerTests()
		{
			_repository = A.Fake<IRepositoryWrapper>();
			_mapper = A.Fake<IMapper>();
			_logger = A.Fake<ILoggerManager>();
			_taskController = new TaskController(_repository, _mapper, _logger);
		}

		[Fact]
		public async void TaskController_GetTasks_ReturnsOkResult()
		{
			// Arrange
			
			// Act
			var actionResult = await _taskController.GetTasks();

			// Assert
			var okObjectResult = actionResult as OkObjectResult;
			Assert.IsType<OkObjectResult>(okObjectResult);	
		}

		[Fact]
		public async void TaskController_GetTasks_ReturnsItems()
		{
			// Arrange
			var tasks = A.Fake<IEnumerable<ModelTask>>();
			var taskList = A.Fake<IEnumerable<TaskDto>>();
			A.CallTo(() => _repository.Task.GetTasks()).Returns(tasks);
			A.CallTo(() => _mapper.Map<IEnumerable<TaskDto>>(tasks)).Returns(new List<TaskDto>() { new TaskDto(), new TaskDto() });

			// Act
			var actionResult = await _taskController.GetTasks();

			// Assert
			OkObjectResult? okObjectResult = actionResult as OkObjectResult;

			var resultValue = (okObjectResult).Value as IEnumerable<TaskDto>;
			Assert.IsType<List<TaskDto>>(resultValue);
			resultValue.Should().HaveCount(2);
		}

		[Fact]
		public async void TaskController_GetTasksById_ReturnsOkResult()
		{
			// Arrange
			int taskId = 1;
			ModelTask task = A.Fake<ModelTask>();
			A.CallTo(() => _repository.Task.IsTaskExist(taskId)).Returns(true);
			A.CallTo(() => _repository.Task.GetTaskById(taskId)).Returns(task);
			A.CallTo(() => _mapper.Map<TaskDto>(task)).Returns(new TaskDto()
			{
				TaskName = "New task"
			});

			// Act
			var actionResult = await _taskController.GetTaskById(taskId);

			// Assert
			var okObjectResult = actionResult as OkObjectResult;
			Assert.IsType<OkObjectResult>(okObjectResult);
		}

		[Fact]
		public async void TaskController_GetTasksById_ReturnsNotFoundResult()
		{
			// Arrange
			int taskId = -1;
			ModelTask task = A.Fake<ModelTask>();
			A.CallTo(() => _repository.Task.IsTaskExist(taskId)).Returns(false);
			A.CallTo(() => _repository.Task.GetTaskById(taskId)).Returns(task);
			A.CallTo(() => _mapper.Map<TaskDto>(task)).Returns(new TaskDto()
			{
				TaskName = "New task"
			});

			// Act
			var actionResult = await _taskController.GetTaskById(taskId);

			// Assert
			var notFoundResult = actionResult as NotFoundResult;
			Assert.IsType<NotFoundResult>(notFoundResult);
		}

		[Fact]
		public async void TaskController_GetTasksById_ReturnsRightItem()
		{
			// Arrange
			int taskId = 1;
			ModelTask task = A.Fake<ModelTask>();
			TaskDto expectedResult = new TaskDto()
			{
				TaskName = "New task"
			};

			A.CallTo(() => _repository.Task.IsTaskExist(taskId)).Returns(true);
			A.CallTo(() => _repository.Task.GetTaskById(taskId)).Returns(task);
			A.CallTo(() => _mapper.Map<TaskDto>(task)).Returns(expectedResult);

			// Act
			var actionResult = await _taskController.GetTaskById(taskId);

			// Assert
			var okObjectResult = actionResult as OkObjectResult;
			var item = okObjectResult.Value as TaskDto;

			Assert.IsType<TaskDto>(item);	
			Assert.Equal(expectedResult, item);
		}

		[Fact]
		public async void TaskController_CreateTask_ReturnsCreatedAtRouteResult()
		{
			// Arrange 
			TaskForCreationDto taskForCreation = A.Fake<TaskForCreationDto>();

			// Act
			var actionResult = await _taskController.CreateTask(taskForCreation);

			// Assert
			var createdAtRouteResult = actionResult as CreatedAtRouteResult;
			Assert.IsType<CreatedAtRouteResult>(createdAtRouteResult);
		}

		[Fact]
		public async void TaskController_CreateTask_ReturnsBadRequest()
		{
			// Arrange 
			TaskForCreationDto taskForCreation = null;

			// Act
			var actionResult = await _taskController.CreateTask(taskForCreation);

			// Assert
			var badRequestResult = actionResult as BadRequestObjectResult;
			Assert.IsType<BadRequestObjectResult>(badRequestResult);
		}

		[Fact]
		public async void TaskController_CreateTask_ReturnsCreatedItem()
		{
			// Arrange 
			TaskForCreationDto taskForCreation = A.Fake<TaskForCreationDto>();
			ModelTask taskEntity = A.Fake<ModelTask>();
			// TaskDto taskCreated = A.Fake<TaskDto>();

			A.CallTo(() => _mapper.Map<ModelTask>(taskForCreation)).Returns(taskEntity);

			// A.CallTo(() => _mapper.Map<ModelTask>(taskForCreation)).Returns(taskEntity);
			A.CallTo(() => _mapper.Map<TaskDto>(taskEntity)).Returns(new TaskDto()
			{
				TaskName = "New task"
			});

			// Act
			var actionResult = await _taskController.CreateTask(taskForCreation);
			var createdAtRouteResult = actionResult as CreatedAtRouteResult;
			var item = createdAtRouteResult.Value as TaskDto;

			// Assert
			Assert.IsType<TaskDto>(item);
			Assert.Equal("New task", item.TaskName);
		}

		[Fact]
		public async void TaskController_DeleteTask_ReturnsNoContent()
		{
			// Arrange 
			int taskId = 1;
			A.CallTo(() => _repository.Task.IsTaskExist(taskId)).Returns(true);

			// Act
			var actionResult = await _taskController.DeleteTask(taskId);

			// Assert
			var noContentResult = actionResult as NoContentResult;
			Assert.IsType<NoContentResult>(noContentResult);
		}

		[Fact]
		public async void TaskController_DeleteTask_ReturnsNotFound()
		{
			// Arrange 
			int taskId = 1;
			A.CallTo(() => _repository.Task.IsTaskExist(taskId)).Returns(false);

			// Act
			var actionResult = await _taskController.DeleteTask(taskId);

			// Assert
			var notFoundResult = actionResult as NotFoundResult;
			Assert.IsType<NotFoundResult>(notFoundResult);
		}

		[Fact]
		public async void TaskController_UpdateTask_ReturnsNoContent()
		{
			// Arrange 
			TaskForUpdateDto taskForUpdate = A.Fake<TaskForUpdateDto>();
			int taskId = 1;
			A.CallTo(() => _repository.Task.IsTaskExist(taskId)).Returns(true);

			// Act
			var actionResult = await _taskController.UpdateTask(taskId, taskForUpdate);

			// Assert
			var noContentResult = actionResult as NoContentResult;
			Assert.IsType<NoContentResult>(noContentResult);
		}

		[Fact]
		public async void TaskController_UpdateTask_ReturnsNotFound()
		{
			// Arrange 
			TaskForUpdateDto taskForUpdate = A.Fake<TaskForUpdateDto>();
			int taskId = 1;
			A.CallTo(() => _repository.Task.IsTaskExist(taskId)).Returns(false);

			// Act
			var actionResult = await _taskController.UpdateTask(taskId, taskForUpdate);

			// Assert
			var notFoundResult = actionResult as NotFoundResult;
			Assert.IsType<NotFoundResult>(notFoundResult);
		}

		[Fact]
		public async void TaskController_UpdateTask_ReturnsBadRequest()
		{
			// Arrange 
			TaskForUpdateDto taskForUpdate = null;
			int taskId = 1;
			A.CallTo(() => _repository.Task.IsTaskExist(taskId)).Returns(true);

			// Act
			var actionResult = await _taskController.UpdateTask(taskId, taskForUpdate);

			// Assert
			var badRequestResult = actionResult as BadRequestObjectResult;
			Assert.IsType<BadRequestObjectResult>(badRequestResult);
		}
	}
}
