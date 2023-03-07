using Contracts;
using Entities;

namespace Repository
{
	public class RepositoryWrapper : IRepositoryWrapper
	{
		private readonly CodeCompetitionContext _codeCompetitionContext;
		private ITaskRepository _task;
		public RepositoryWrapper(CodeCompetitionContext context )
		{
			_codeCompetitionContext = context;
		}
		public async Task Save()
		{
			await _codeCompetitionContext.SaveChangesAsync();
		}

		public ITaskRepository Task
		{
			get
			{
				if (_task == null )
				{
					_task = new TaskRepository(_codeCompetitionContext);	
				}
				return _task;	
			}
		}
	}
}
