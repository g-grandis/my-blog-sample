using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.LearnByDoing.MyBlog.BL.Tests.Unit.Helpers
{
    public static class TagsHelper
    {

        public static List<Tag> GetDefaultMockData()
        {
            List<Tag> mockTags = new List<Tag>();
            mockTags.Add(new Tag()
            {
                Id = 1,
                Name = "This is a name for Tag 1",
                Description = "This is a description for Tag 1"
            });
            mockTags.Add(new Tag()
            {
                Id = 2,
                Name = "This is a name for Tag 2",
                Description = "This is a description for Tag 2"
            });
            return mockTags;
        }
        public static List<Tag> GetPaginatedDefaultMockData()
        {
            List<Tag> mockTags = new List<Tag>();
            mockTags.Add(new Tag()
            {
                Id = 1,
                Name = "This is a name for tag 1",
                Description = "This is a description for tag 1"
            });
            mockTags.Add(new Tag()
            {
                Id = 2,
                Name = "This is a name for tag 2",
                Description = "This is a description for tag 2"
            });
            mockTags.Add(new Tag()
            {
                Id = 3,
                Name = "This is a name for tag 3",
                Description = "This is a description for tag 3"
            });
            mockTags.Add(new Tag()
            {
                Id = 4,
                Name = "This is a name for tag 4",
                Description = "This is a description for tag 4"
            });
            return mockTags;
        }
        public static List<Tag> GetMockDataWithPosts(List<Post> mockPosts)
        {
            List<Tag> mockTags = TagsHelper.GetDefaultMockData();
            mockTags[0].Posts = new List<Domain.Models.Post>();
            mockTags[0].Posts.Add(mockPosts[0]);
            mockTags[1].Posts = new List<Domain.Models.Post>();
            mockTags[1].Posts.Add(mockPosts[1]);
            return mockTags;
        }
    }
}
