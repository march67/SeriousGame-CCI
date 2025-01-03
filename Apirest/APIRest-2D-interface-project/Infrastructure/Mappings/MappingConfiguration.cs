using APIRest_2D_interface_project.Infrastructure.Mappings.Profiles;
using APIRest_2D_interface_project.Infrastructure.Services.Implementations;
using APIRest_2D_interface_project.Infrastructure.Services.Interfaces;

namespace APIRest_2D_interface_project.Infrastructure.Mappings
{
    public static class MappingConfiguration
    {
        public static IServiceCollection AddMappingConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHashingService, PasswordHashingService>();

            services.AddAutoMapper(config =>
            {
                config.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
            });

            return services;
        }
    }
}
