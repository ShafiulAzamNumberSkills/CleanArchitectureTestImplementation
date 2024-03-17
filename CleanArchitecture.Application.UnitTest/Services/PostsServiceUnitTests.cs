using AutoFixture;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ViewModels;
using CleanArchitecture.Infrastructure.IRepositories;
using FluentAssertions;
using Moq;

namespace CleanArchitecture.Application.UnitTest
{
    public class PostsServiceUnitTests
    {
        private Fixture _fixture;
        private Mock<IPostsRepository> _postsRepositoryMock;
        private PostsService _postsService;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _postsRepositoryMock = _fixture.Freeze<Mock<IPostsRepository>>();
            _postsService = new PostsService(_postsRepositoryMock.Object);

        }

        [Test]
        public async Task GetListOfPostsAsResponseOnGetPostsMethodCallAsync()
        {
            //Arrange

            //Act
            var response = await _postsService.GetPosts();

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ResponseModel>();
            Assert.IsAssignableFrom<Post[]>(response.Data);
            _postsRepositoryMock.Verify(p => p.GetPosts(), Times.Once());
            Assert.Pass();
        }


        [Test]
        public async Task GetPostAsResponseOnGetPostByIDMethodCallByValidIdAsync()
        {
            //Arrange
            int id = _fixture.Create<int>();
            _postsRepositoryMock.Setup(p => p.GetPostByID(It.IsAny<int>())).ReturnsAsync(new Post());

            //Act
            var response = await _postsService.GetPostByID(id);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ResponseModel>();
            response.Data.Should().BeOfType<Post>();
            _postsRepositoryMock.Verify(r => r.GetPostByID(id), Times.Once);
            Assert.Pass();
        }


        [Test]
        public async Task GetNullAsResponseOnGetPostByIDMethodCallByInvalidIdAsync()
        {
            //Arrange
            int id = 0; //invalid id
            Post post = null;
            _postsRepositoryMock.Setup(p => p.GetPostByID(id)).ReturnsAsync(post);

            //Act
            var response = await _postsService.GetPostByID(id);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ResponseModel>();
            response.Data.Should().BeNull();
            _postsRepositoryMock.Verify(r => r.GetPostByID(id), Times.Once);
            Assert.Pass();
        }

        [Test]
        public async Task GetTrueAsResponseOnAddPostMethodCallWithValidDataAsync()
        {
            //Arrange
            Post post = new Post()
            {
                Title = "Test",
            };
            _postsRepositoryMock.Setup(p => p.InsertPost(post)).ReturnsAsync(1);

            //Act
            var response = await _postsService.InsertPost(post);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ResponseModel>();
            response.Data.Should().Be(1);
            _postsRepositoryMock.Verify(p => p.InsertPost(post), Times.Once());
            Assert.Pass();
        }


        [Test]
        public async Task GetValidationErrorAsResponseOnAddPostCallWithEmptyTitleAsync()
        {
            //Arrange
            Post post = new Post()
            {
                Title = "",
            };

            //Act
            var response = await _postsService.InsertPost(post);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ResponseModel>();
            response.ValidationErrors.Should().HaveCount(1);
            _postsRepositoryMock.Verify(p => p.InsertPost(post), Times.Never());
            Assert.Pass();
        }


        [Test]
        public async Task GetTrueAsResponseOnUpdatePostMethodCallWithValidDataAsync()
        {
            //Arrange
            Post post = new Post()
            {
                Id = 1,
                Title = "Test",
            };
            _postsRepositoryMock.Setup(x => x.GetPostByID(It.IsAny<int>()))
                .ReturnsAsync(post);
            _postsRepositoryMock.Setup(x => x.UpdatePost(post))
                .ReturnsAsync(true);

            //Act
            var response = await _postsService.UpdatePost(post);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ResponseModel>();
            response.Data.Should().Be(true);
            _postsRepositoryMock.Verify(p => p.GetPostByID(post.Id), Times.Once());
            _postsRepositoryMock.Verify(p => p.UpdatePost(post), Times.Once());
            Assert.Pass();
        }

        [Test]
        public async Task  PostShouldNotUpdateOnUpdatePostMethodCallWithInvalidIdAsync()
        {
            //Arrange
            Post post = new Post()
            {
                Id = 0, //invalid id
                Title = "Test",
            };
            Post nullPost = null;
            _postsRepositoryMock.Setup(x => x.GetPostByID(post.Id)).ReturnsAsync(nullPost); 

            //Act
            var response = await _postsService.UpdatePost(post);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ResponseModel>();
            _postsRepositoryMock.Verify(p => p.GetPostByID(post.Id), Times.Once());
            _postsRepositoryMock.Verify(p => p.UpdatePost(post), Times.Never());
            Assert.Pass();
        }

        [Test]
        public async Task GetValidationErrorAsResponseOnUpdatePostCallWithEmptyTitleAsync()
        {
            //Arrange
            Post post = new Post()
            {
                Title = "",
            };

            //Act
            var response = await _postsService.UpdatePost(post);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ResponseModel>();
            response.ValidationErrors.Should().HaveCount(1);
            _postsRepositoryMock.Verify(p => p.UpdatePost(post), Times.Never());
            Assert.Pass();
        }


        [Test]
        public async Task GetTrueAsResponseOnDeletePostMethodCallWithValidIdAsync()
        {
            //Arrange
            int id = _fixture.Create<int>();
            _postsRepositoryMock.Setup(p => p.GetPostByID(id)).ReturnsAsync(new Post());
            _postsRepositoryMock.Setup(p => p.DeletePost(id)).Returns(true);

            //Act
            var response = await _postsService.DeletePost(id);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ResponseModel>();
            response.Data.Should().Be(true);
            _postsRepositoryMock.Verify(p => p.GetPostByID(id), Times.Once());
            _postsRepositoryMock.Verify(p => p.DeletePost(id), Times.Once());
            Assert.Pass();
        }


        [Test]
        public async Task PostShouldNotDeleteOnDeletePostMethodCallWithInvalidIdAsync()
        {
            //Arrange
            int id = 0; //invalid Id
            Post nullPost = null;
            _postsRepositoryMock.Setup(p => p.GetPostByID(id)).ReturnsAsync(nullPost);

            //Act
            var response = await _postsService.DeletePost(id);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ResponseModel>();
            _postsRepositoryMock.Verify(p => p.GetPostByID(id), Times.Once());
            _postsRepositoryMock.Verify(p => p.DeletePost(id), Times.Never);
            Assert.Pass();
        }
    }
}