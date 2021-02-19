using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System.Data;
using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories;

namespace Magicianred.LearnByDoing.MyBlog.DAL.Repositories
{
    public class TagsRepository : ITagsRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly IConfiguration _configuration;

        public TagsRepository(IDatabaseConnectionFactory connectionFactory, IConfiguration configuration)
        {
            this._connectionFactory = connectionFactory;
            this._configuration = configuration;
        }
        public IEnumerable<Tag> GetAll()
        {
            IEnumerable<Tag> tags = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                tags = connection.Query<Tag>("SELECT Id, Name, Description FROM tags ORDER BY CreateDate DESC");
            }
            return tags;
        }
        public IEnumerable<Tag> GetPaginatedAll(int page, int pageSize)
        {
            int skip = 0;
            if (page <= 0)
            {
                page = 1;
            }
            page--;
            skip = page * pageSize;
            IEnumerable<Tag> tags = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                var databaseType = _configuration.GetSection("DatabaseType").Value;
                if (!string.IsNullOrWhiteSpace(databaseType) && databaseType.ToLower().Trim() == "mysql")
                {
                    tags = connection.Query<Tag>("SELECT Id, Name, Description FROM Tags ORDER BY CreateDate  DESC LIMIT @offset, @pageSize ",
                        new { offset = skip, pageSize = pageSize });
                }
                else // MSSQL DB
                {
                    tags = connection.Query<Tag>("SELECT Id, Name, Description FROM Tags ORDER BY CreateDate DESC OFFSET @offset ROWS FETCH NEXT @PageSize ROWS ONLY",
                            new { offset = skip, pageSize = pageSize });
                }
            }
            return tags;
        }

        public Tag GetById(int id)
        {
            Tag tag = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                // TOP 1 is not a command for SQLite, remove
                tag = connection.QueryFirstOrDefault<Tag>("SELECT Id, Name, Description FROM Tags WHERE Id = @TagId", new { TagId = id });
                if (tag != null)
                {
                    tag.Posts = connection.Query<Post>("SELECT c.Id, c.Title, c.Text, c.Author FROM Tags a INNER JOIN Posttags b on a.Id = b.TagId INNER JOIN Posts c on b.PostId = c.Id WHERE a.Id = @TagId", new { TagId = id }).AsList<Post>();
                }
            }
            return tag;
        }
    }
}
