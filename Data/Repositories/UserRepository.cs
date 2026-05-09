using Core.Entities;
using Core.Interfaces.IRepositories;
using Data.Context;

namespace Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository 
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
