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
    /// <summary>
    /// Repository of posts
    /// </summary>
    public class PostsRepository : IPostsRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly IConfiguration _configuration;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public PostsRepository(IDatabaseConnectionFactory connectionFactory, IConfiguration configuration)
        {
            this._connectionFactory = connectionFactory;
            this._configuration = configuration;

        }

        /// <summary>
        /// Retrieve all Posts items
        /// </summary>
        /// <returns>list of post</returns>
        public IEnumerable<Post> GetAll()
        {
            IEnumerable<Post> posts = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                posts = connection.Query<Post>("SELECT Id, Title, Text, Author FROM Posts ORDER BY CreateDate DESC");
            }
            return posts;
        }

        public IEnumerable<Post> GetPaginatedAll(int page,int pageSize)
        {
            int skip = 0;
            if (page <= 0)
            {
                page=1;
            }
            page--;
            skip = page * pageSize;
            IEnumerable<Post> posts = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                var databaseType = _configuration.GetSection("DatabaseType").Value;
                if (!string.IsNullOrWhiteSpace(databaseType) && databaseType.ToLower().Trim() == "mysql")
                {
                    posts = connection.Query<Post>(
                        "SELECT Id, Title, Text, Author FROM Posts ORDER BY CreateDate DESC LIMIT @offset, @pageSize ",
                        new { offset = skip, pageSize = pageSize });
                }
                else // if (!string.IsNullOrWhiteSpace(databaseType) && databaseType.ToLower().Trim() == "mssql")
                {
                    posts = connection.Query<Post>(
                            "SELECT Id, Title, Text, Author FROM Posts ORDER BY CreateDate DESC OFFSET @offset ROWS FETCH NEXT @PageSize ROWS ONLY",
                            new { offset = skip, pageSize = pageSize });
                }
            }
            return posts;
        }
        /// <summary>
        /// Retrieve post by own id
        /// </summary>
        /// <param name="id">id of the post to retrieve</param>
        /// <returns>the post, null if id not found</returns>
        public Post GetById(int id)
        {
            Post post = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                // TOP 1 is not a command for SQLite, remove
                post = connection.QueryFirstOrDefault<Post>("SELECT Id, Title, Text, Author, CategoryId FROM Posts WHERE Id = @PostId", new { PostId = id });
                if (post != null)
                {
                    post.Tags = connection.Query<Tag>("SELECT c.Id, c.Name, c.Description FROM posts a INNER JOIN posttags b on a.Id = b.PostId INNER JOIN tags c on b.TagId = c.Id WHERE a.Id = @PostId", new { PostId = id }).AsList<Tag>();
                }
            }
            return post;
        }
        public IEnumerable<Post> GetAllByAuthor(string author)
        {
            IEnumerable<Post> posts = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                // TOP 1 is not a command for SQLite, remove
                posts = connection.Query<Post>("SELECT Id, Title, Text, Author FROM posts WHERE Author = @Author", new { Author = author });
                
            }
            return posts;
        }
    }
}
