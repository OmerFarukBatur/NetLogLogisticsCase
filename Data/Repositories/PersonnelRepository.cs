using Core.Entities;
using Core.Interfaces.IRepositories;
using Data.Context;

namespace Data.Repositories
{
    public class PersonnelRepository : Repository<Personnel>, IPersonnelRepository
    {
        public PersonnelRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
