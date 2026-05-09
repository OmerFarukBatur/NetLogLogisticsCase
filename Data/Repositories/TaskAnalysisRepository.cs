using Core.Entities;
using Core.Interfaces.IRepositories;
using Data.Context;

namespace Data.Repositories
{
    public class TaskAnalysisRepository : Repository<TaskAnalysis>, ITaskAnalysisRepository
    {
        public TaskAnalysisRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}