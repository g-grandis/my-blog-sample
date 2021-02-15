﻿using Magicianred.LearnByDoing.MyBlog.DAL.Repositories;
using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories;
using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Services;
using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Magicianred.LearnByDoing.MyBlog.BL.Services
{
    public class CategoriesService : ICategoriesService
    {
        private ICategoriesRepository _categoriesRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        /// <summary>
        /// Retrieve all posts
        /// </summary>
        /// <returns>list of posts</returns>
        public List<Category> GetAll()
        {
            return _categoriesRepository.GetAll().ToList();
        }

        /// <summary>
        /// Retrieve the post by own id
        /// </summary>
        /// <param name="id">id of post to retrieve</param>
        /// <returns>the post, null if id not found</returns>
        public Category GetById(int id)
        {
            return _categoriesRepository.GetById(id);
        }
    }
}
