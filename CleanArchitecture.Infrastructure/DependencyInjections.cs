using CleanArchitecture.Application;
using CleanArchitecture.Application.IRepositories;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CleanArchitecture.Infrastructure
{
    public static class DependencyInjections
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {


            var connectionString = configuration.GetConnectionString("DefaultConnection");


            services.AddDbContext<PostContext>(options =>
                 options.UseSqlServer(connectionString));

            services.AddScoped<IPostsRepository, PostsRepository>();


            services.AddApplicationServices(configuration);


            return services;
        }


    }
}
