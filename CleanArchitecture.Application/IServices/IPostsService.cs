using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ViewModels;

namespace CleanArchitecture.Application.IServices
{
    public interface IPostsService
    {
        Task<ResponseModel> GetPosts();
        Task<ResponseModel> GetPostByID(int ID);
        Task<ResponseModel> InsertPost(Post objPost);
        Task<ResponseModel> UpdatePost(Post objPost);
        Task<ResponseModel> DeletePost(int ID);
    }
}
