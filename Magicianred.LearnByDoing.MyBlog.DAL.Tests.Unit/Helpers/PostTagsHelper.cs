using Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Helpers
{
    public static class PostTagsHelper
    {
        public static List<PostTag> GetDefaultMockData()
        {
            List<PostTag> mockPostTags = new List<PostTag>();
            mockPostTags.Add(new PostTag()
            {
                PostId = 1,
                TagId = 1,
                CreateDate = new DateTime(2021,02,17)
            });
            mockPostTags.Add(new PostTag()
            {
                PostId = 2,
                TagId = 2,
                CreateDate = new DateTime(2021, 02, 18)
            });

            return mockPostTags;
        }
    }
}
