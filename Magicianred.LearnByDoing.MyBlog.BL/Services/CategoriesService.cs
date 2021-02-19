using Magicianred.LearnByDoing.MyBlog.DAL.Repositories;
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

        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public List<Category> GetAll()
        {
            return _categoriesRepository.GetAll().ToList();
        }

        public Category GetById(int id)
        {
            return _categoriesRepository.GetById(id);
        }
        public List<Category> GetPaginatedAll(int page, int pageSize)
        {
            return _categoriesRepository.GetPaginatedAll(page, pageSize).ToList();
        }
    }
}
