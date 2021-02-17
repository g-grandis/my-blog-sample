using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories
{
    public interface ITagsRepository
    {
        public IEnumerable<Tag> GetAll();
        public Tag GetById(int id);
    }
}
