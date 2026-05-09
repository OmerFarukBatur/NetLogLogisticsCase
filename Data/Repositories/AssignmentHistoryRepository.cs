using Core.Entities;
using Core.Interfaces.IRepositories;
using Data.Context;

namespace Data.Repositories
{
    public class AssignmentHistoryRepository : Repository<AssignmentHistory>, IAssignmentHistoryRepository
    {
        public AssignmentHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
