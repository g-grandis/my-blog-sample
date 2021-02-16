using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Models
{
    [Alias("Tags")]
    public class Tag : Magicianred.LearnByDoing.MyBlog.Domain.Models.Tag
    {
        public DateTime CreateDate { get; set; }
    }
}
