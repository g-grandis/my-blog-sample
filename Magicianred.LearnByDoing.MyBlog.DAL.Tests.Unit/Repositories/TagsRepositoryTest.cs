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
    public class TagsRepositoryTest
    {

        private TagsRepository _sut;

        private IDatabaseConnectionFactory _connectionFactory;


        #region SetUp and TearDown

        [OneTimeSetUp]
        public void SetupUpOneTime()
        {
            // Instance of mock
            _connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            _sut = new TagsRepository(_connectionFactory);
        }

        [OneTimeTearDown]
        public void TearDownOneTime()
        {
             //dispose
            //_tagsRepository = null;
        }

        #endregion

        [Test]
        [Category("Unit test")]
        public void should_retrieve_all_tags()
        {
            // Arrange
            var mockTags = TagsHelper.GetDefaultMockData();
            var db = new InMemoryDatabase();
            db.Insert<Tag>(mockTags);
            _connectionFactory.GetConnection().Returns(db.OpenConnection());



            // Act
            var tags = _sut.GetAll();
            var tagsList = tags.ToList();

            // Assert
            Assert.IsNotNull(tags);
            Assert.AreEqual(tags.Count(), mockTags.Count);

            mockTags = mockTags.OrderBy(o => o.Id).ToList();
            tagsList = tagsList.OrderBy(o => o.Id).ToList();

            for (var i = 0; i < mockTags.Count; i++)
            {
                var mockTag = mockTags[0];
                var tag = tagsList[0];
                Assert.IsTrue(mockTag.Id == tag.Id);
                Assert.IsTrue(mockTag.Name == tag.Name);
                Assert.IsTrue(mockTag.Description == tag.Description);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [Category("Unit test")]
        public void should_retrieve_tag_by_id(int id)
        {
            // Arrange
            var mockTags = TagsHelper.GetDefaultMockData();
            var mockPosts = PostsHelper.GetDefaultMockData();
            var mockPostTags = PostTagsHelper.GetDefaultMockData();
            var db = new InMemoryDatabase();
            db.Insert<Tag>(mockTags);
            db.Insert<Post>(mockPosts);
            db.Insert<PostTag>(mockPostTags);

            var mockTag = mockTags.Where(x => x.Id == id).FirstOrDefault();

            // Act
            var tag = _sut.GetById(id);

            // Assert
            Assert.IsNotNull(tag);

            Assert.IsTrue(mockTag.Id == tag.Id);
            Assert.IsTrue(mockTag.Name == tag.Name);
            Assert.IsTrue(mockTag.Description == tag.Description);

        }

        [TestCase(1)]
        [TestCase(2)]
        [Category("Unit test")]
        public void should_retrieve_tag_by_id_with_post(int id)
        {
            // Arrange
            var mockTags = TagsHelper.GetDefaultMockData();
            var mockPosts = PostsHelper.GetDefaultMockData();
            var mockPostTags = PostTagsHelper.GetDefaultMockData();
            var db = new InMemoryDatabase();
            db.Insert<Tag>(mockTags);
            db.Insert<Post>(mockPosts);
            db.Insert<PostTag>(mockPostTags);

            var mockTag = mockTags.Where(x => x.Id == id).FirstOrDefault();

            // Act
            var tag = _sut.GetById(id);

            // Assert
            Assert.IsNotNull(tag);

            Assert.IsTrue(mockTag.Id == tag.Id);
            Assert.IsTrue(mockTag.Name == tag.Name);
            Assert.IsTrue(mockTag.Description == tag.Description);
            Assert.IsTrue(mockTag.Posts == tag.Posts);

        }

        [Test]
        [Category("Unit test")]
        public void should_retrieve_no_one_tag()
        {
            // Arrange
            var mockTags = TagsHelper.GetDefaultMockData();
            var db = new InMemoryDatabase();
            db.Insert<Tag>(mockTags);
            _connectionFactory.GetConnection().Returns(db.OpenConnection());

            // Act
            var tag = _sut.GetById(1000);

            // Assert
            Assert.IsNull(tag);

        }
    }
}

