using CleanArchitecture.Application.IServices;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace CleanArchitecture.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostsService _postsService;
        public PostController(IPostsService postsService)
        {
            _postsService = postsService ??
                throw new ArgumentNullException(nameof(Post));
        }
        [HttpGet]
        [Route("GetPost")]
        public async Task<IActionResult> GetPosts()
        {
            return Ok(await _postsService.GetPosts());
        }
        [HttpGet]
        [Route("GetPostByID/{Id}")]
        public async Task<IActionResult> GetPostByID(int Id)
        {
            return Ok(await _postsService.GetPostByID(Id));
        }
        [HttpPost]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost(Post dep)
        {
            return Ok(await _postsService.InsertPost(dep));
        }
        [HttpPut]
        [Route("UpdatePost")]
        public async Task<IActionResult> UpdatePost(Post dep)
        {
            return Ok(await _postsService.UpdatePost(dep));
        }
        [HttpDelete]
        //[HttpDelete("{id}")]
        [Route("DeletePost")]
        public async Task<IActionResult> DeletePost(int id)
        {
            return Ok(await _postsService.DeletePost(id));
        }
    }
}
