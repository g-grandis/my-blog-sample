﻿using Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Helpers;
using Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Models;
using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories;
using Magicianred.LearnByDoing.MyBlog.DAL.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using System.Linq;

namespace Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Repositories
{
    [TestFixture]
    public class CategoriesRepositoryTest
    {
        private CategoriesRepository _sut;

        private IDatabaseConnectionFactory _connectionFactory;


        #region SetUp and TearDown

        [OneTimeSetUp]
        public void SetupUpOneTime()
        {
            // Instance of mock
            _connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            _sut = new CategoriesRepository(_connectionFactory);
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
        public void should_retrieve_all_categories()
        {
            // Arrange
            var mockCategories = CategoriesHelper.GetDefaultMockData();
            var db = new InMemoryDatabase();
            db.Insert<Category>(mockCategories);
            _connectionFactory.GetConnection().Returns(db.OpenConnection());

            // Act
            var categories = _sut.GetAll();
            var categoriesList = categories.ToList();

            // Assert
            Assert.IsNotNull(categories);
            Assert.AreEqual(categories.Count(), mockCategories.Count);

            mockCategories = mockCategories.OrderBy(o => o.Id).ToList();
            categoriesList = categoriesList.OrderBy(o => o.Id).ToList();

            for (var i = 0; i < mockCategories.Count; i++)
            {
                var mockCategory = mockCategories[0];
                var category = categoriesList[0];
                Assert.IsTrue(mockCategory.Id == category.Id);
                Assert.IsTrue(mockCategory.Name == category.Name);
                Assert.IsTrue(mockCategory.Description == category.Description);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [Category("Unit test")]
        public void should_retrieve_post_by_id(int id)
        {
            // Arrange
            var mockCategories = CategoriesHelper.GetDefaultMockData();
            var db = new InMemoryDatabase();
            db.Insert<Category>(mockCategories);
            _connectionFactory.GetConnection().Returns(db.OpenConnection());

            var mockCategory = mockCategories.Where(x => x.Id == id).FirstOrDefault();

            // Act
            var category = _sut.GetById(id);

            // Assert
            Assert.IsNotNull(category);

            Assert.IsTrue(mockCategory.Id == category.Id);
            Assert.IsTrue(mockCategory.Name == category.Name);
            Assert.IsTrue(mockCategory.Description == category.Description);

        }

        [Test]
        [Category("Unit test")]
        public void should_retrieve_no_one_post()
        {
            // Arrange
            var mockCategories = CategoriesHelper.GetDefaultMockData();
            var db = new InMemoryDatabase();
            db.Insert<Category>(mockCategories);
            _connectionFactory.GetConnection().Returns(db.OpenConnection());

            // Act
            var category = _sut.GetById(1000);

            // Assert
            Assert.IsNull(category);

        }
    }
}

