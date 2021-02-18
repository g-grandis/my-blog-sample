using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Models
{
    public interface IPost
    {
        int Id { get; set; }
        string Title { get; set; }
        string Text { get; set; }
        int CategoryId { get; set; }
        public List<Tag> Tags { get; set; }
        string Author { get; set; }

    }
}
