using System.Globalization;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Gl.Manager.Validator.Customer;
using Gl.Manager.Validator.User;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Gl.WebApi.Configuration
{
    public static class FluentValidationConfig
    {
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(x =>
                {
                    x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    x.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
                .AddJsonOptions(p =>
                {
                    p.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddFluentValidation(p =>
                {
                    p.RegisterValidatorsFromAssemblyContaining<NewCustomerValidator>();
                    p.RegisterValidatorsFromAssemblyContaining<EditCustomerValidator>();
                    p.RegisterValidatorsFromAssemblyContaining<NewUserValidator>();
                    // p.RegisterValidatorsFromAssemblyContaining<EditUserValidator>();
                    p.ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-BR");
                });
        }
    }
}