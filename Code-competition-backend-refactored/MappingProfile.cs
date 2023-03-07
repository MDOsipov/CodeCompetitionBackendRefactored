using AutoMapper;
using Entities.DataTransferObjects;
using ModelTask = Entities.Models.Task;

namespace Code_competition_backend_refactored
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<ModelTask, TaskDto>();
			CreateMap<ModelTask, TaskWithCategoryInfoDto>()
				.ForMember(dest =>
					dest.TaskCategoryName,
					opt => opt.MapFrom(src => src.TaskCategory.CategoryName));
			CreateMap<ModelTask, TaskForTeamInfoDto>()
				.ForMember(dest =>
					dest.TaskCategoryName,
					opt => opt.MapFrom(src => src.TaskCategory.CategoryName))
				.ForMember(dest =>
					dest.StartTime,
					opt => opt.MapFrom(src => src.TaskToTeams.FirstOrDefault().StartTime))
				.ForMember(dest =>
					dest.EndTime,
					opt => opt.MapFrom(src => src.TaskToTeams.FirstOrDefault().EndTime))
				.ForMember(dest =>
					dest.GithubUrl,
					opt => opt.MapFrom(src => src.TaskToTeams.FirstOrDefault().GithubUrl))
				.ForMember(dest =>
					dest.SubmitterComment,
					opt => opt.MapFrom(src => src.TaskToTeams.FirstOrDefault().SubmitterComment))
				.ForMember(dest =>
					dest.CompetitionAdminComment,
					opt => opt.MapFrom(src => src.TaskToTeams.FirstOrDefault().CompetitionAdminComment))
				.ForMember(dest =>
					dest.ParticipantIdForTask,
					opt => opt.MapFrom(src => src.TaskToTeams.FirstOrDefault().ParticipantIdForTask))
				.ForMember(dest =>
					dest.ParticipantEmailForTask,
					opt => opt.MapFrom(src => src.TaskToTeams.FirstOrDefault().ParticipantIdForTaskNavigation.Email))
				.ForMember(dest =>
					dest.TaskStatusId,
					opt => opt.MapFrom(src => src.TaskToTeams.FirstOrDefault().TaskStatusId))
				.ForMember(dest =>
					dest.ReachedScore,
					opt => opt.MapFrom(src => src.TaskToTeams.FirstOrDefault().ReachedScore));

			CreateMap<TaskForCreationDto, ModelTask>();
			CreateMap<TaskForUpdateDto, ModelTask>();
		}
	}
}
