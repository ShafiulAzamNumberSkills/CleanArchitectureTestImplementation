using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Entities;


namespace CleanArchitecture.Domain.Data
{
    public class PostContext : DbContext
    {

        public PostContext() { }

        public PostContext(DbContextOptions<PostContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
