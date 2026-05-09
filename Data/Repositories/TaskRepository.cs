using Core.Interfaces.IRepositories;
using Data.Context;

namespace Data.Repositories
{
    public class TaskRepository : Repository<Core.Entities.Task>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}