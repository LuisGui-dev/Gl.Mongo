using Gl.Manager.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace Gl.WebApi.Configuration
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserMappingProfile),
                typeof(NewUserMappingProfile),
                typeof(CustomerMappingProfile),
                typeof(NewCustomerMappingProfile),
                typeof(EditCustomerMappingProfile));
        }
    }
}