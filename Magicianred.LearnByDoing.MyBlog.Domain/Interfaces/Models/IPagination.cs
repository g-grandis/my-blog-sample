using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Models
{
    public interface IPagination
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}
