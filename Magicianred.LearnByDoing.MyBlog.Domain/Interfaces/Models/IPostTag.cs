using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Models
{
    public interface IPostTag
    {
        int PostId { get; set; }
        int TagId { get; set; }

    }
}
