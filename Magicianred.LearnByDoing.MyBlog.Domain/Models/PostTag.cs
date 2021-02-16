using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Models
{
    public partial class PostTag : IPostTag
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
    }
}
