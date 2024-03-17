using AutoFixture;
using CleanArchitecture.Domain.Data;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.IRepositories;
using CleanArchitecture.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.UnitTest.Repositories
{
    public class PostsRepositoryUnitTest
    {


        private IFixture _fixture;
        private PostContext _dbContextMock;
        private IPostsRepository _postsRepository;



        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();

            var options = new DbContextOptionsBuilder<PostContext>().UseInMemoryDatabase(databaseName: "TestPostDB").Options;


            _dbContextMock = new PostContext(options);

            _dbContextMock.Posts.AddRange(
                    new List<Post>()
                    {
                        new Post { Id= 0,Title = "Title 1", Description = "Description 1" },
                        new Post { Id= 0,Title = "Title 2", Description = "Description 2"  },
                        new Post { Id= 0,Title = "Title 3", Description = "Description 3"  },
                    }
                  );

            _dbContextMock.SaveChanges();


            _postsRepository = new PostsRepository(_dbContextMock);
        }


        [Test]
        public async Task GetPostsShouldReturnDataIfDataFoundSucessfullyAsync()
        {
            //Arrange

            //Act
            var result = await _postsRepository.GetPosts();

            //Assert
            Assert.NotNull(result);
            result.Should().BeAssignableTo<IEnumerable<Post>>();

        }


        [Test]
        public async Task GetPostByIdShouldReturnDataIfDataFoundSucessfullyAsync()
        {
            //Arrange
            int id = _dbContextMock.Posts.FirstOrDefault().Id;

            //Act
            var data = await _postsRepository.GetPostByID(id);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(data, Is.Not.Null);
                Assert.That(data.Id, Is.EqualTo(id));
            });

        }

        [Test]
        public async Task GetPostByIdShouldReturnNullIfDataNotFoundAsync()
        {
            //Arrange
            int id = 0; //invalid id

            //Act
            var data = await _postsRepository.GetPostByID(id);

            //Assert
            data.Should().BeNull();

        }

        [Test]
        public async Task CreatePostsShouldReturnNonZeroIdWhenDataInsertedSucessfullyAsync()
        {
            //Arrange
            var data = _fixture.Create<Post>();

            //Act
            var result = await _postsRepository.InsertPost(data);

            //Assert
            Assert.NotNull(result);
            result.Should().BeGreaterThanOrEqualTo(1);

        }

        [Test]
        public async Task CreatePostsShouldReturnZeroIdWhenInvalidDataProvidedAsync()
        {
            //Arrange
            var data = new Post()
            {
                Title = null //invalid value
            };

            //Act
            var result = await _postsRepository.InsertPost(data);

            //Assert
            Assert.NotNull(result);
            result.Should().Be(0);

        }

        [Test]
        public async Task UpdatePostsShouldReturnTrueWhenDataIsUpdatedSucessfullyAsync()
        {
            //Arrange
            var data = _dbContextMock.Posts.FirstOrDefault();

            data.Title = "Name Updated";

            //Act
            var result = await _postsRepository.UpdatePost(data);

            //Assert
            Assert.NotNull(result);
            result.Should().BeTrue();
            Assert.IsTrue(data.Title == _dbContextMock.Posts.FirstOrDefault().Title);
        }


        [Test]
        public async Task UpdatePostsShouldReturnFalseWhenWrongDataProvidedAsync()
        {
            //Arrange
            var data = new Post()
            {
                Id = 0,  //wrong Id
                Title = "Valid Title",
            };

            //Act
            var result = await _postsRepository.UpdatePost(data);

            //Assert
            Assert.NotNull(result);
            result.Should().BeFalse();

        }



        [Test]
        public void DeleteShouldReturnTrueIfDataDeleteSucessfully()
        {
            //Arrange
            int id = _dbContextMock.Posts.FirstOrDefault().Id;

            //Act
            var result = _postsRepository.DeletePost(id);

            //Assert
            Assert.IsTrue(result);
            Assert.IsTrue(_dbContextMock.Posts.FirstOrDefault().Id != id);

        }


        [Test]
        public void DeleteShouldReturnFalseIfWrongIdProvided()
        {
            //Arrange
            int id = 0; //Wrong Id

            //Act
            var result = _postsRepository.DeletePost(id);

            //Assert
            Assert.IsFalse(result);


        }

    }
}