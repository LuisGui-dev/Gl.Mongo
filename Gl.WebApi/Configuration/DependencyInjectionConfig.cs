using Gl.Data;
using Gl.Data.Repositories;
using Gl.Manager.Implementation;
using Gl.Manager.Interfaces.Manager;
using Gl.Manager.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Gl.WebApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<MongoDb>();
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserManager, UserManager>();
            
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerManager, CustomerManager>();
            
        }
    }
}