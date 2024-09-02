using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CustomerWebApi.Configuration.ServiceConfiguration
{
    public static class MainConfigurations
    {
        public static void AddAutoMapperProfiles(this IServiceCollection serviceCollection)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            // Add the assemblies where your AutoMapper profiles are defined
            var autoMapperAssemblies = new List<Assembly>
            {
                Assembly.GetExecutingAssembly(),
                Assembly.Load("CustomerService")// Add current assembly
                // Add other assemblies if needed, e.g.,
                // Assembly.Load("AnotherAssemblyWithProfiles")
            };

            // Register AutoMapper with the specified assemblies
            serviceCollection.AddAutoMapper(autoMapperAssemblies);
        }
        public static void AddSwaggerGenAuthorization(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

                // Add the JWT token security definition
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Please enter the word 'Bearer' followed by a space and then the JWT token.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // Add the JWT token security requirement
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                  {
                      new OpenApiSecurityScheme
                      {
                          Reference = new OpenApiReference
                          {
                              Type = ReferenceType.SecurityScheme,
                              Id = "Bearer"
                          }
                      },
                      new List<string>()
                  }});
            });
        }
    }
}

