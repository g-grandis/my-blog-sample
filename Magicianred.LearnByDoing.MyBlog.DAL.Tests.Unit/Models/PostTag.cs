using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Models
{
    [Alias("posttags")]
    public class PostTag : Magicianred.LearnByDoing.MyBlog.Domain.Models.PostTag
    {
        public DateTime CreateDate { get; set; }
    }
}
