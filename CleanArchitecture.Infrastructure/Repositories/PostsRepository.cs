using CleanArchitecture.Domain.Data;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly PostContext _postDBContext;
        public PostsRepository(PostContext context)
        {
            _postDBContext = context ??
                throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _postDBContext.Posts.ToListAsync();
        }
        public async Task<Post> GetPostByID(int ID)
        {
            return await _postDBContext.Posts.FindAsync(ID);
        }
        public async Task<int> InsertPost(Post objPost)
        {
            try
            {
                _postDBContext.Posts.Add(objPost);
                await _postDBContext.SaveChangesAsync();
                return objPost.Id;
            }
            catch (Exception ex) { 
            
                return 0;
            
            }
        }
        public async Task<bool> UpdatePost(Post objPost)
        {
            try
            {
                _postDBContext.Entry(objPost).State = EntityState.Modified;
                await _postDBContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex) {

                return false;

            }

        }
        public bool DeletePost(int ID)
        {
            try
            {
                _postDBContext.Posts.Remove(_postDBContext.Posts.Find(ID));
                _postDBContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
