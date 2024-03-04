using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Repositories;
using API_ArtworkSharingPlatform.Services.Interfaces;
using API_ArtworkSharingPlatform.Services.Services;


namespace API_ArtworkSharingPlatforms
{
	static class DependencyInjection
	{
		public static IServiceCollection AddApiWebService(this IServiceCollection services)
		{
			services.AddScoped<IArtworkRepository, ArtworkRepository>();
			services.AddScoped<IArtworkService, ArtworkService>();

			services.AddScoped<IReportRepository, ReportRepository>();
			services.AddScoped<IReportService, ReportService>();

			services.AddScoped<IPersonRepository, PersonRepository>();
			services.AddScoped<IPersonService, PersonService>();

			services.AddScoped<IRatestarRepository, RateStarRepository>();
			services.AddScoped<IRatestarService, RatestarService>();

			services.AddScoped<ICommentRepository, CommentRepository>();
			services.AddScoped<ICommentService, CommentService>();

			services.AddScoped<IFollowRepository, FollowRepository>();
			services.AddScoped<IFollowService, FollowService>();

			services.AddScoped<INotificationRepository, NotificationRepository>();
			services.AddScoped<INotificationService, NotificationService>();

			services.AddScoped<IRequestRepository, RequestRepository>();
			services.AddScoped<IRequestService, RequestService>();

			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped<IOrderService, OrderService>();

			services.AddScoped<IShoppingCartRepository,ShoppingCartRepository>();
			services.AddScoped<IShoppingCartService,ShoppingCartService>();

			return services;
		}
	}
}
