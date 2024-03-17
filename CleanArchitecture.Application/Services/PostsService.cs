using CleanArchitecture.Application.IServices;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ViewModels;
using CleanArchitecture.Infrastructure.IRepositories;

namespace CleanArchitecture.Application.Services
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository _postsRepository;
        public PostsService(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository ??
                throw new ArgumentNullException(nameof(postsRepository));
        }
        public async Task<ResponseModel> GetPosts()
        {
            return ResponseModel.ok(await _postsRepository.GetPosts());
        }
        public async Task<ResponseModel> GetPostByID(int ID)
        {

            var post = await _postsRepository.GetPostByID(ID);

            if(post == null) {

                return ResponseModel.customError("No Post Found!");

            }
            return ResponseModel.ok(post);
        }
        public async Task<ResponseModel> InsertPost(Post objPost)
        {
            var validationErrors = new Dictionary<string, string>();

            if (String.IsNullOrEmpty(objPost.Title))
            {
                validationErrors["Title"] = "Title can not be empty.";
                return ResponseModel.validationErrors(validationErrors);
            }

            int id = await _postsRepository.InsertPost(objPost);

            if (id != 0)
            {
                return ResponseModel.ok(id);

            }
            else
            {
                return ResponseModel.customError("Unexpected Error");
            }
           
        }
        public async Task<ResponseModel> UpdatePost(Post objPost)
        {

            var validationErrors = new Dictionary<string, string>();


            if (String.IsNullOrEmpty(objPost.Title))
            {
                validationErrors["Title"] = "Title can not be empty.";
                return ResponseModel.validationErrors(validationErrors);
            }

            var post = await _postsRepository.GetPostByID(objPost.Id);

            if (post == null)
            {

                return ResponseModel.customError("No Post Found!");

            }

            bool result = await _postsRepository.UpdatePost(objPost);

            if (!result)
            {

                return ResponseModel.customError("Unexpected Error");

            }

            return ResponseModel.ok(result);
        }
        public async Task<ResponseModel> DeletePost(int ID)
        {
            var post = await _postsRepository.GetPostByID(ID);

            if (post == null)
            {

                return ResponseModel.customError("No Post Found!");

            }

            bool response =  _postsRepository.DeletePost(ID);

            if (!response)
            {

                return ResponseModel.customError("Unexpected Error");

            }

            return ResponseModel.ok(response);
        }


    }
}
