using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.IRepositories
{
    public interface IPostsRepository
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPostByID(int ID);
        Task<int> InsertPost(Post objPost);
        Task<bool> UpdatePost(Post objPost);
        bool DeletePost(int ID);
    }
}
