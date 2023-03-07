using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelTask = Entities.Models.Task;
using Task = System.Threading.Tasks.Task;
using TaskCategory = Entities.Models.TaskCategory;

namespace CodeCompetitionBackend.test.RepositoryTest.cs
{
	public class TaskRepositoryTests : IDisposable
	{
		protected readonly CodeCompetitionContext _context;
        public TaskRepositoryTests()
		{
			var options = new DbContextOptionsBuilder<CodeCompetitionContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;

            _context = new CodeCompetitionContext(options);
            _context.Database.EnsureCreated();
		}

		[Fact]
		public async void TaskRepository_GetTasks_ReturnsTasks()
		{
			// Arrange
			var taskRepository = new TaskRepository(_context);
            await AddMockData();

            // Act
            var tasksResult = await taskRepository.GetTasks();

			// Assert
			tasksResult.Should().NotBeNull();
			tasksResult.Should().BeOfType<List<ModelTask>>();
			tasksResult.Should().HaveCount(2);
		}

        [Fact]
        public async void TaskRepository_GetTaskById_ReturnsTask()
        {
            // Arrange
            var taskRepository = new TaskRepository(_context);
            await AddMockData();

            // Act
            var tasksResult = await taskRepository.GetTaskById(1);

            // Assert
            tasksResult.Should().NotBeNull();
            tasksResult?.TaskContent.Should().BeEquivalentTo("New task content1");
            tasksResult.Should().BeOfType<ModelTask>();   
        }

        [Fact]
        public async void TaskRepository_GetTaskByName_ReturnsTask()
        {
            // Arrange
            var taskRepository = new TaskRepository(_context);
            await AddMockData();

            // Act
            var tasksResult = await taskRepository.GetTaskByName("New task2");

            // Assert
            tasksResult.Should().NotBeNull();
            tasksResult?.TaskContent.Should().BeEquivalentTo("New task content2");
            tasksResult.Should().BeOfType<ModelTask>();
        }

        [Fact]
        public async void TaskRepository_GetTaskByName_ReturnsTrue()
        {
            // Arrange
            var taskRepository = new TaskRepository(_context);
            await AddMockData();
            int taskId = 2;

            // Act
            var tasksResult = await taskRepository.IsTaskExist(taskId);

            // Assert
            tasksResult.Should().BeTrue();
        }

        [Fact]
        public async void TaskRepository_GetTasksWithCategory_ReturnsTasks()
        {
            // Arrange
            var taskRepository = new TaskRepository(_context);
            await AddMockData();

            // Act
            var tasksResult = await taskRepository.GetTasksWithCategory();

            // Assert
            tasksResult.Should().NotBeNull();
            tasksResult.Should().BeOfType<List<ModelTask>>();
            tasksResult.Should().HaveCount(2);
            tasksResult.ToList()[0]?.TaskCategory?.CategoryName.Should().BeEquivalentTo("Some category");
        }

        [Fact]
        public async void TaskRepository_GetTasksByCompetitionId_ReturnsTasks()
        {
            // Arrange
            await AddMockData();
            var taskRepository = new TaskRepository(_context);
            int competitionId = 2;

            // Act
            var tasksResult = await taskRepository.GetTasksByCompetitionId(competitionId);

            // Assert
            tasksResult.Should().NotBeNull();
            tasksResult.Should().BeOfType<List<ModelTask>>();
            tasksResult.Should().HaveCount(1);
            tasksResult.ToList().FirstOrDefault()?.Id.Should().Be(2);
        }

        [Fact]
        public async void TaskRepository_GetTasksByTaskCategory_ReturnsTasks()
        {
            // Arrange
            await AddMockData();
            var taskRepository = new TaskRepository(_context);
            int taskCategoryId = 2;


            // Act
            var tasksResult = await taskRepository.GetTasksByTaskCategory(taskCategoryId);

            // Assert
            tasksResult.Should().NotBeNull();
            tasksResult.Should().BeOfType<List<ModelTask>>();
            tasksResult.Should().HaveCount(1);
            tasksResult.ToList().FirstOrDefault()?.Id.Should().Be(2);
        }

        [Fact]
        public async void TaskRepository_GetTasksInProgressByTeamId_ReturnsTasks()
        {
            // Arrange
            await AddMockData();
            var taskRepository = new TaskRepository(_context);
            int teamId = 2;


            // Act
            var tasksResult = await taskRepository.GetTasksInProgressByTeamId(teamId);

            // Assert
            tasksResult.Should().NotBeNull();
            tasksResult.Should().BeOfType<List<ModelTask>>();
            tasksResult.Should().HaveCount(1);
            tasksResult.ToList().FirstOrDefault()?.Id.Should().Be(2);
        }

        [Fact]
        public async void TaskRepository_GetTasksSubmittedByCompetitionIdAndTeamId_ReturnsTasks()
        {
            // Arrange
            await AddMockData();
            var taskRepository = new TaskRepository(_context);
            int teamId = 1;
            int competitionId = 1;

            // Act
            var tasksResult = await taskRepository.GetTasksSubmittedByCompetitionIdAndTeamId(competitionId, teamId);

            // Assert
            tasksResult.Should().NotBeNull();
            tasksResult.Should().BeOfType<List<ModelTask>>();
            tasksResult.Should().HaveCount(1);
            tasksResult.ToList().FirstOrDefault()?.Id.Should().Be(1);
        }

        [Fact]
        public async void TaskRepository_GetTasksSubmittedByTeamId_ReturnsTasks()
        {
            // Arrange
            await AddMockData();
            var taskRepository = new TaskRepository(_context);
            int teamId = 1;

            // Act
            var tasksResult = await taskRepository.GetTasksSubmittedByTeamId(teamId);

            // Assert
            tasksResult.Should().NotBeNull();
            tasksResult.Should().BeOfType<List<ModelTask>>();
            tasksResult.Should().HaveCount(1);
            tasksResult.ToList().FirstOrDefault()?.Id.Should().Be(1);
        }

        [Fact]
        public async void TaskRepository_IsTaskExistById_ReturnsTrue()
        {
            // Arrange
            await AddMockData();
            var taskRepository = new TaskRepository(_context);
            int taskId = 1;

            // Act
            var tasksResult = await taskRepository.IsTaskExist(taskId);

            // Assert
            tasksResult.Should().BeTrue();
        }

        [Fact]
        public async void TaskRepository_IsTaskExistById_ReturnsFalse()
        {
            // Arrange
            await AddMockData();
            var taskRepository = new TaskRepository(_context);
            int taskId = 5;

            // Act
            var tasksResult = await taskRepository.IsTaskExist(taskId);

            // Assert
            tasksResult.Should().BeFalse();
        }

        [Fact]
        public async void TaskRepository_IsTaskExistByName_ReturnsTrue()
        {
            // Arrange
            await AddMockData();
            var taskRepository = new TaskRepository(_context);
            string taskName = "New task1";

            // Act
            var tasksResult = await taskRepository.IsTaskExist(taskName);

            // Assert
            tasksResult.Should().BeTrue();
        }

        [Fact]
        public async void TaskRepository_IsTaskExistByName_ReturnsFalse()
        {
            // Arrange
            await AddMockData();
            var taskRepository = new TaskRepository(_context);
            string taskName = "New task133";

            // Act
            var tasksResult = await taskRepository.IsTaskExist(taskName);

            // Assert
            tasksResult.Should().BeFalse();
        }

        [Fact]
        public async void TaskRepository_AddTask_AddsTask()
        {
            // Arrange
            await AddMockData();
            var taskRepository = new TaskRepository(_context);
            ModelTask newTask = new ModelTask()
            {
                Id = 3,
                TaskName = "New task3",
                TaskDescription = "New task desc3",
                TaskContent = "New task content3",
                TaskCategoryId = 3,
                Timeframe = new TimeSpan(),
                Points = 0,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateUserId = 1,
                UpdateUserId = 1,
                StatusId = 1
            };

            // Act
            taskRepository.AddTask(newTask);
            await _context.SaveChangesAsync();

            // Assert
            var tasks = await taskRepository.GetTasks();
            tasks.Should().HaveCount(3);
        }

        [Fact]
        public async void TaskRepository_DeleteTask_DeletesTask()
        {
            // Arrange
            await AddMockData();
            var taskRepository = new TaskRepository(_context);

            ModelTask newTask = new ModelTask()
            {
                Id = 3,
                TaskName = "New task3",
                TaskDescription = "New task desc3",
                TaskContent = "New task content3",
                TaskCategoryId = 3,
                Timeframe = new TimeSpan(),
                Points = 0,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateUserId = 1,
                UpdateUserId = 1,
                StatusId = 1
            };

            // Act
            taskRepository.AddTask(newTask);
            await _context.SaveChangesAsync();

            _context.Entry(newTask).State = EntityState.Detached;

            taskRepository.DeleteTask(newTask);
            await _context.SaveChangesAsync();

            // Assert
            var tasks = await taskRepository.GetTasks();
            tasks.Should().HaveCount(2);
        }

        [Fact]
        public async void TaskRepository_UpdateTask_UpdatesTask()
        {
            // Arrange
            await AddMockData();
            var taskRepository = new TaskRepository(_context);

            ModelTask newTask = new ModelTask()
            {
                Id = 3,
                TaskName = "New task3",
                TaskDescription = "New task desc3",
                TaskContent = "New task content3",
                TaskCategoryId = 3,
                Timeframe = new TimeSpan(),
                Points = 0,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateUserId = 1,
                UpdateUserId = 1,
                StatusId = 1
            };

            // Act
            taskRepository.AddTask(newTask);
            await _context.SaveChangesAsync();

            _context.Entry(newTask).State = EntityState.Detached;

            newTask.TaskName = "Task name updated";
            taskRepository.UpdateTask(newTask);
            await _context.SaveChangesAsync();

            // Assert
            var task = await taskRepository.GetTaskById(3);
            task?.TaskName.Should().BeEquivalentTo("Task name updated");
        }

        private async Task AddMockData()
		{
            if (await _context.Tasks.CountAsync() <= 0)
            {
                _context.Tasks.AddRange(
                    new ModelTask()
                    {
                        Id = 1,
                        TaskName = "New task1",
                        TaskDescription = "New task desc",
                        TaskContent = "New task content1",
                        TaskCategoryId = 1,
                        Timeframe = new TimeSpan(),
                        Points = 0,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        CreateUserId = 1,
                        UpdateUserId = 1,
                        StatusId = 1,
                        TaskCategory = new TaskCategory()
                        {
                            Id = 1,
                            CategoryName = "Some category",
                            CategoryDescription = "Some description",
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            CreateUserId = 1,
                            UpdateUserId = 1,
                            StatusId = (int)Enums.Status.Active
                        },
                        TaskToCompetitions = new List<TaskToCompetition>()
                        {
                            new TaskToCompetition()
                            {
                                Id = 1,
                                CompetitionId = 1,
                                TaskId = 1,
                                StatusId = (int)Enums.Status.Active,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                                CreateUserId = 1,
                                UpdateUserId = 1
                            }
                        },
                        TaskToTeams = new List<TaskToTeam>()
                        {
                            new TaskToTeam()
                            {
                                Id = 1,
                                TeamId = 1,
                                TaskId = 1,
                                TaskStatusId = (int)Enums.TaskStatus.Submitted,
                                StatusId = (int)Enums.Status.Active,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                                CreateUserId = 1,
                                UpdateUserId = 1, 
                                ReachedScore = 0
                            }
                        }

                    }, 
                    new ModelTask()
                    {
                        Id = 2,
                        TaskName = "New task2",
                        TaskDescription = "New task desc",
                        TaskContent = "New task content2",
                        TaskCategoryId = 1,
                        Timeframe = new TimeSpan(),
                        Points = 0,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        CreateUserId = 1,
                        UpdateUserId = 1,
                        StatusId = 1,
                        TaskCategory = new TaskCategory()
                        {
                            Id = 2,
                            CategoryName = "Some category2",
                            CategoryDescription = "Some description2",
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now,  
                            CreateUserId = 1,
                            UpdateUserId = 1,
                            StatusId = (int)Enums.Status.Active
                        },
                        TaskToCompetitions = new List<TaskToCompetition>()
                        {
                            new TaskToCompetition()
                            {
                                Id = 2,
                                CompetitionId = 2,
                                TaskId = 2,
                                StatusId = (int)Enums.Status.Active,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                                CreateUserId = 1,
                                UpdateUserId = 1
                            }
                        },
                        TaskToTeams = new List<TaskToTeam>()
                        {
                            new TaskToTeam()
                            {
                                Id = 2,
                                TeamId = 2,
                                TaskId = 2,
                                TaskStatusId = (int)Enums.TaskStatus.InProgress,
                                StatusId = (int)Enums.Status.Active,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                                CreateUserId = 1,
                                UpdateUserId = 1,
                                ReachedScore = 0
                            }
                        }
                    }
                );

                await _context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
	}
}
