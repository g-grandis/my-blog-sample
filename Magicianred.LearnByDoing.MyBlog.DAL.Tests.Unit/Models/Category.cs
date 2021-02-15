using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Models
{
    [Alias("Categories")]
    public class Category : Magicianred.LearnByDoing.MyBlog.Domain.Models.Category
    {
        public DateTime CreateDate { get; set; }
    }
}
