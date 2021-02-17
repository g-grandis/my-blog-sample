using Magicianred.LearnByDoing.MyBlog.DAL.Repositories;
using Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Helpers;
using Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Models;
using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Repositories
{
    [TestFixture]
    public class PostsRepositoryTest
    {
        /// <summary>
        /// PostsService is our System Under Test
        /// </summary>
        private PostsRepository _sut;

        private IDatabaseConnectionFactory _connectionFactory;


        #region SetUp and TearDown

        [OneTimeSetUp]
        public void SetupUpOneTime()
        {
            // Instance of mock
            _connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            _sut = new PostsRepository(_connectionFactory);
        }

        [OneTimeTearDown]
        public void TearDownOneTime()
        {
            // dispose
            //_postsRepository = null;
        }

        #endregion

        [Test]
        [Category("Unit test")]
        public void should_retrieve_all_posts()
        {
            // Arrange
            var mockTags = TagsHelper.GetDefaultMockData();
            var mockPosts = PostsHelper.GetDefaultMockData();
            var mockPostTags = PostTagsHelper.GetDefaultMockData();
            var db = new InMemoryDatabase();
            db.Insert<Tag>(mockTags);
            db.Insert<Post>(mockPosts);
            db.Insert<PostTag>(mockPostTags);

            _connectionFactory.GetConnection().Returns(db.OpenConnection());


            // Act
            var posts = _sut.GetAll();
            var postsList = posts.ToList();

            // Assert
            Assert.IsNotNull(posts);
            Assert.AreEqual(posts.Count(), mockPosts.Count);

            mockPosts = mockPosts.OrderBy(o => o.Id).ToList();
            postsList = postsList.OrderBy(o => o.Id).ToList();

            for (var i = 0; i < mockPosts.Count; i++)
            {
                var mockPost = mockPosts[0];
                var post = postsList[0];
                Assert.IsTrue(mockPost.Id == post.Id);
                Assert.IsTrue(mockPost.Title == post.Title);
                Assert.IsTrue(mockPost.Text == post.Text);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [Category("Unit test")]
        public void should_retrieve_post_by_id(int id)
        {
            // Arrange
            var mockTags = TagsHelper.GetDefaultMockData();
            var mockPosts = PostsHelper.GetDefaultMockData();
            var mockPostTags = PostTagsHelper.GetDefaultMockData();
            var db = new InMemoryDatabase();
            db.Insert<Tag>(mockTags);
            db.Insert<Post>(mockPosts);
            db.Insert<PostTag>(mockPostTags);

            _connectionFactory.GetConnection().Returns(db.OpenConnection());


            var mockPost = mockPosts.Where(x => x.Id == id).FirstOrDefault();

            // Act
            var post = _sut.GetById(id);

            // Assert
            Assert.IsNotNull(post);

            Assert.IsTrue(mockPost.Id == post.Id);
            Assert.IsTrue(mockPost.Title == post.Title);
            Assert.IsTrue(mockPost.Text == post.Text);

        }

        [TestCase(1)]
        [TestCase(2)]
        [Category("Unit test")]
        public void should_retrieve_post_by_id_with_tag(int id)
        {
            // Arrange
            var mockTags = TagsHelper.GetDefaultMockData();
            var mockPosts = PostsHelper.GetDefaultMockData();
            var mockPostTags = PostTagsHelper.GetDefaultMockData();
            var mockPostsWithTags = PostsHelper.GetMockDataWithTags(mockTags);
            var db = new InMemoryDatabase();
            var db2 = new InMemoryDatabase();
            db.Insert<Tag>(mockTags);
            db.Insert<Post>(mockPosts);
            db.Insert<PostTag>(mockPostTags);
            db2.Insert<Post>(mockPostsWithTags);

            _connectionFactory.GetConnection().Returns(db.OpenConnection());


            var mockPost = mockPosts.Where(x => x.Id == id).FirstOrDefault();
            var mockPostWithTag = mockPostsWithTags.Where(x => x.Id == id).FirstOrDefault();

            // Act
            var post = _sut.GetById(id);
            var postWTag = _sut.GetById(id);
            // Assert
            Assert.IsNotNull(post);

            Assert.IsTrue(mockPost.Id == post.Id);
            Assert.IsTrue(mockPost.Title == post.Title);
            Assert.IsTrue(mockPost.Text == post.Text);
            Assert.IsTrue(mockPostWithTag.Tags.Count == postWTag.Tags.Count);

        }

        [Test]
        [Category("Unit test")]
        public void should_retrieve_no_one_post()
        {
            // Arrange
            var mockPosts = PostsHelper.GetDefaultMockData();
            var db = new InMemoryDatabase();
            db.Insert<Post>(mockPosts);
            _connectionFactory.GetConnection().Returns(db.OpenConnection());

            // Act
            var post = _sut.GetById(1000);

            // Assert
            Assert.IsNull(post);

        }
    }
}
