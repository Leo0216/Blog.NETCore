using Blog.EntityFramework;
using Blog.EntityFramework.Enum;
using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _dbContext;
        public HomeController(DataContext dbContext, ILogger<HomeController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IActionResult> Index([FromServices]IConfiguration configuration)
        {
            ViewData["currentPage"] = "Home";
            if (configuration["AppSettings:CloseComment"] != null && bool.Parse(configuration["AppSettings:CloseComment"]))
            {
                ViewData["Title"] = "新不落阁";
            }
            else
            {
                ViewData["Title"] = "不落阁 - 一个.NET程序员的个人博客";
            }

            IndexModel indexModel = new IndexModel();
            //公告
            indexModel.Announcements = await _dbContext.Announcement
                .Where(s => s.Status != -1)
                .OrderByDescending(s => s.CreateTime)
                .ToListAsync();
            //轮播
            indexModel.Carousels = await _dbContext.Carousel
                .Where(s => s.Status != -1)
                .ToListAsync();
            //最新前10文章
            indexModel.Articles = await _dbContext.Article
                .Where(s => s.Status == (int)CommonStatus.Valid && !s.IsTop)
                .Select(s => new Article()
                {
                    Id = s.Id,
                    CreateTime = s.CreateTime,
                    Title = s.Title,
                    Abstract = s.Abstract,
                    ReadCount = s.ReadCount,
                    RemarkCount = s.RemarkCount,
                    PraiseCount = s.PraiseCount,
                    IsRecommend = s.IsRecommend,
                    Cover = s.Cover,
                    Category = s.Category
                })
                .OrderByDescending(s => s.CreateTime)
                .Take(10)
                .ToListAsync();
            //置顶文章
            indexModel.SetTopArticles = await _dbContext.Article
                .Where(s => s.Status == (int)CommonStatus.Valid && s.IsTop)
                .Select(s => new Article()
                {
                    Id = s.Id,
                    CreateTime = s.CreateTime,
                    Title = s.Title,
                    Abstract = s.Abstract,
                    ReadCount = s.ReadCount,
                    RemarkCount = s.RemarkCount,
                    PraiseCount = s.PraiseCount,
                    IsRecommend = s.IsRecommend,
                    Cover = s.Cover,
                    Category = s.Category
                })
                .OrderByDescending(s => s.CreateTime)
                .ToListAsync();
            //推荐前8文章
            indexModel.RecommendArticles = await _dbContext.Article
                .Where(s => s.Status == (int)CommonStatus.Valid && s.IsRecommend)
                .Select(s => new Article()
                {
                    Id = s.Id,
                    CreateTime = s.CreateTime,
                    Title = s.Title
                })
                .OrderByDescending(s => s.CreateTime)
                .Take(8)
                .ToListAsync();
            //热门前8文章
            indexModel.HotArticles = await _dbContext.Article
                .Where(s => s.Status == (int)CommonStatus.Valid)
                .Select(s => new Article()
                {
                    Id = s.Id,
                    CreateTime = s.CreateTime,
                    Title = s.Title,
                    ReadCount = s.ReadCount
                })
                .OrderByDescending(s => s.ReadCount)
                .Take(8)
                .ToListAsync();
            //最新前8评论
            indexModel.NewRemarks = await _dbContext.Remark
                .Where(s => s.Status == (int)CommonStatus.Valid)
                .OrderByDescending(s => s.CreateTime)
                .Include(s => s.User)
                .Take(8)
                .ToListAsync();
            //最新前8留言
            indexModel.NewComments = await _dbContext.Comment
                .Where(s => s.Status != -1)
                .OrderByDescending(s => s.CreateTime)
                .Include(s => s.User)
                .Take(8)
                .ToListAsync();
            //友情链接
            indexModel.FriendLinks = await _dbContext.FriendLink
                .Where(s => s.Status == (int)CommonStatus.Valid)
                .Select(s => new FriendLink()
                {
                    Link = s.Link,
                    Title = s.Title
                }).ToListAsync();
            //文章总数
            indexModel.ArticleCount = await _dbContext.Article.Where(s => s.Status == (int)CommonStatus.Valid).CountAsync();
            //说说总数
            indexModel.TimelineCount = await _dbContext.Timeline.Where(s => s.Status != -1).CountAsync();
            //评论总数
            indexModel.RemarkCount = await _dbContext.Remark.Where(s => s.Status != -1).CountAsync();
            //留言总数
            indexModel.CommentCount = await _dbContext.Comment.Where(s => s.Status != -1).CountAsync();

            return View(indexModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("Home/CustomError/{statusCode?}")]
        public IActionResult CustomError(int statusCode)
        {
            if (statusCode == (int)HttpStatusCode.NotFound)
                return View("Error404");
            return View(statusCode);
        }
    }
}
