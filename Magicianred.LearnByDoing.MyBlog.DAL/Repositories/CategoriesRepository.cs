using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories;
using System.Data;

namespace Magicianred.LearnByDoing.MyBlog.DAL.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly IConfiguration _configuration;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public CategoriesRepository(IDatabaseConnectionFactory connectionFactory, IConfiguration configuration)
        {
            this._connectionFactory = connectionFactory;
            this._configuration = configuration;

        }

        /// <summary>
        /// Retrieve all Posts items
        /// </summary>
        /// <returns>list of post</returns>
        public IEnumerable<Category> GetAll()
        {
            IEnumerable<Category> categories = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                categories = connection.Query<Category>("SELECT Id, Name, Description FROM categories ORDER BY CreateDate DESC");
            }
            return categories;
        }

        public IEnumerable<Category> GetPaginatedAll(int page, int pageSize)
        {
            int skip = 0;
            if (page <= 0)
            {
                page = 1;
            }
            page--;
            skip = page * pageSize;
            IEnumerable<Category> categories = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                var databaseType = _configuration.GetSection("DatabaseType").Value;
                if (!string.IsNullOrWhiteSpace(databaseType) && databaseType.ToLower().Trim() == "mysql")
                {
                    categories = connection.Query<Category>("SELECT Id, Name, Description FROM Categories ORDER BY CreateDate DESC LIMIT @offset, @pageSize ",
                        new { offset = skip, pageSize = pageSize });
                }
                else // MSSQL DB
                {
                    categories = connection.Query<Category>("SELECT Id, Name, Description FROM Categories ORDER BY CreateDate DESC OFFSET @offset ROWS FETCH NEXT @PageSize ROWS ONLY",
                            new { offset = skip, pageSize = pageSize });
                }
            }
            return categories;
        }
        /// <summary>
        /// Retrieve post by own id
        /// </summary>
        /// <param name="id">id of the post to retrieve</param>
        /// <returns>the post, null if id not found</returns>
        public Category GetById(int id)
        {
            Category category = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                // TOP 1 is not a command for SQLite, remove
                category = connection.QueryFirstOrDefault<Category>("SELECT * FROM categories WHERE Id = @CategoryId", new { CategoryId = id });
            }
            return category;
        }

    }
}

