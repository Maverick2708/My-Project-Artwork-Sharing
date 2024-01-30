using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Repositories;
using API_ArtworkSharingPlatform.Services.Interfaces;
using API_ArtworkSharingPlatform.Services.Services;


namespace API_ArtworkSharingPlatforms
{
    static class DependencyInjection
    {
        public static IServiceCollection AddApiWebService (this IServiceCollection services)
        {
            services.AddScoped<IArtworkRepository, ArtworkRepository>();
            services.AddScoped<IArtworkService,ArtworkService>();

            return services;
        }
    }
}
