using Core.Interfaces.IRepositories;
using Data.Context;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class ServiceRegistration
    {
        public static void AddDataServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAssignmentHistoryRepository, AssignmentHistoryRepository>();
            services.AddScoped<IPersonnelRepository, PersonnelRepository>();
            services.AddScoped<ITaskAnalysisRepository, TaskAnalysisRepository>();
            services.AddScoped<ITaskDevelopmentRepository, TaskDevelopmentRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
