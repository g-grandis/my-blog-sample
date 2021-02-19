﻿using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories
{
    public interface IPostsRepository
    {
        public IEnumerable<Post> GetAll();
        Post GetById(int id);
        public IEnumerable<Post> GetAllByAuthor(string author);
        public IEnumerable<Post> GetPaginatedAll(int page, int pageSize);

    }
}
