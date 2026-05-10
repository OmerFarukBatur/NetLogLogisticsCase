using Business.Mapping;
using Business.Services;
using Core.Interfaces.IServices;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Business
{
    public static class ServiceRegistration
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AuthProfile>();
            }, AppDomain.CurrentDomain.GetAssemblies());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
