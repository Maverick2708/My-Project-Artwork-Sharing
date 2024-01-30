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

            services.AddScoped<IReportRepository,ReportRepository>();
            services.AddScoped<IReportService,ReportService>();

            services.AddScoped<IPersonRepository,PersonRepository>();
            services.AddScoped<IPersonService,PersonService>();

            return services;
        }
    }
}
