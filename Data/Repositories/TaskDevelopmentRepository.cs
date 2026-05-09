using Core.Entities;
using Core.Interfaces.IRepositories;
using Data.Context;

namespace Data.Repositories
{
    public class TaskDevelopmentRepository : Repository<TaskDevelopment>, ITaskDevelopmentRepository
    {
        public TaskDevelopmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}