using Entities;
using Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
	public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{
		protected CodeCompetitionContext codeCompetitionContext { get; set; }

		public RepositoryBase(CodeCompetitionContext context)
		{
			codeCompetitionContext = context;
		}

		public IQueryable<T> FindAll() => codeCompetitionContext.Set<T>().AsNoTracking();
		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => codeCompetitionContext.Set<T>().Where(expression).AsNoTracking();
		public void Create(T entity) => codeCompetitionContext.Set<T>().Add(entity);
		public void Update(T entity) => codeCompetitionContext.Set<T>().Update(entity);	
		public void Delete(T entity) => codeCompetitionContext.Set<T>().Remove(entity);
		
	}
}
