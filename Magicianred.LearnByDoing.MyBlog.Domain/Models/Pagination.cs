using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Models
{
    public partial class Pagination : IPagination
    {
        public Pagination()
        {
            this.PageNumber = 1;
            this.PageSize = 3;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
