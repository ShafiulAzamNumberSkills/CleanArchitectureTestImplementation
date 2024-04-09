using CleanArchitecture.Application.IServices;
using CleanArchitecture.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CleanArchitecture.Application
{ 
    public static class DependencyInjections
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddScoped<IPostsService, PostsService>();


            return services;
        }


    }
}
