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
                TagId = 2
            });
            mockPostTags.Add(new PostTag()
            {
                PostId = 3,
                TagId = 1
            });

            return mockPostTags;
        }
    }
}
