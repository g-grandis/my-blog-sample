using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Magicianred.LearnByDoing.MyBlog.Web.Models;
using Magicianred.LearnByDoing.MyBlog.BL.Services;
using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Services;
using Magicianred.LearnByDoing.MyBlog.Domain.Models;

namespace Magicianred.LearnByDoing.MyBlog.Web.Controllers
{
    /// <summary>
    /// Handle Posts of blog
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostsService _postsService;
        private readonly ICategoriesService _categoriesService;
        private readonly ITagsService _tagsService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="postsService"></param>
        /// <param name="logger"></param>
        public HomeController(IPostsService postsService, ICategoriesService categoriesService, ITagsService tagsService, ILogger<HomeController> logger)
        {
            _postsService = postsService;
            _categoriesService = categoriesService;
            _tagsService = tagsService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieve all Posts
        /// GET: <HomeController>
        /// </summary>
        /// <returns>list of Posts</returns>
        public IActionResult Index(int page = 1, int pageSize = 3,string author = null)
        {

            if (!String.IsNullOrWhiteSpace(author))
            {
                var posts = _postsService.GetAllByAuthor(author);
                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;
                ViewBag.IsFirst = ((int)ViewBag.CurrentPage > 1);
                ViewBag.IsLast = (posts.Count >= (int)ViewBag.PageSize);
                return View(posts);
            }
            else
            {
                var posts = _postsService.GetPaginatedAll(page,pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;
                ViewBag.IsFirst = ((int)ViewBag.CurrentPage > 1);
                ViewBag.IsLast = (posts.Count >= (int)ViewBag.PageSize);
                return View(posts);
            }
            
        }

        /// <summary>
        /// Retrieve the post with the id
        /// GET <HomeController>/Post/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the post with requested id</returns>
        public IActionResult Post(int id)
        {
            var post = _postsService.GetById(id);
            return View(post);
        }
        
        public IActionResult Categories(int page = 1, int pageSize = 3)
        {
            var categories = _categoriesService.GetPaginatedAll(page, pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.IsFirst = ((int)ViewBag.CurrentPage > 1);
            ViewBag.IsLast = (categories.Count >= (int)ViewBag.PageSize);
            return View(categories);
        }
        public IActionResult Category(int id)
        {
            var category = _categoriesService.GetById(id);
            return View(category);
        }

        public IActionResult Tags(int page = 1, int pageSize = 3)
        {
            var tags = _tagsService.GetPaginatedAll(page, pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.IsFirst = ((int)ViewBag.CurrentPage > 1);
            ViewBag.IsLast = (tags.Count >= (int)ViewBag.PageSize);
            return View(tags);
        }
        public IActionResult Tag(int id)
        {
            var tags = _tagsService.GetById(id);
            return View(tags);
        }

        /// <summary>
        /// Show about page
        /// </summary>
        /// <returns></returns>
        public IActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Show contact page
        /// </summary>
        /// <returns></returns>
        public IActionResult Contact()
        {
            return View();
        }

        /// <summary>
        /// Show error in page
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
