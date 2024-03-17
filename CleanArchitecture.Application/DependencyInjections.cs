using CleanArchitecture.Application.IServices;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CleanArchitecture.Application
{ 
    public static class DependencyInjections
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddInfrastructureServices(configuration);

            services.AddScoped<IPostsService, PostsService>();


            return services;
        }


    }
}
