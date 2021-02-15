using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System.Collections.Generic;

namespace Magicianred.LearnByDoing.MyBlog.BL.Tests.Unit.Helpers
{
    public static class CategoriesHelper
    {
        public static List<Category> GetDefaultMockData()
        {
            List<Category> mockCategories = new List<Category>();
            mockCategories.Add(new Category()
            {
                Id = 1,
                Name = "This is a name for category 1",
                Description = "This is a description for category 1"
            });
            mockCategories.Add(new Category()
            {
                Id = 2,
                Name = "This is a name for category 2",
                Description = "This is a description for category 2"
            });
            return mockCategories;
        }
    }
}
