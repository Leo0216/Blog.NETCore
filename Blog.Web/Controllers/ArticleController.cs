using Blog.EntityFramework;
using Blog.EntityFramework.Enum;
using Blog.Web.Models;
using Leo.Framework.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Similarity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly DataContext _dbContext;
        private IDistributedCache _cache;
        private ILogger _logger;
        public ArticleController(DataContext dbContext, IDistributedCache cache, IConfiguration configuration, ILogger<ArticleController> logger)
        {
            _dbContext = dbContext;
            _cache = cache;
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; set; }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewData["currentPage"] = "Article";
            base.OnActionExecuted(context);
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [Route("Article/All/{pageIndex?}")]
        public async Task<IActionResult> All(int pageIndex = 0)
        {
            try
            {
                int pageSize = 10;

                ArticleListModel articleListModel = new ArticleListModel();

                articleListModel.Articles = await _dbContext.Article
                    .Where(s => s.Status == (int)CommonStatus.Valid)
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
                        Category = new ArticleCategory()
                        {
                            Name = s.Category.Name,
                            Id = s.CategoryId
                        }
                    })
                    .OrderByDescending(s => s.CreateTime)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                articleListModel.RecommendArticles = await _dbContext.Article
                    .Where(s => s.IsRecommend && s.Status == (int)CommonStatus.Valid)
                    .Select(s => new Article()
                    {
                        Id = s.Id,
                        CreateTime = s.CreateTime,
                        Title = s.Title
                    })
                    .OrderByDescending(s => s.CreateTime)
                    .ToListAsync();

                articleListModel.RandomArticles = await _dbContext.Article
                    .Where(s => s.Status == (int)CommonStatus.Valid)
                    .Select(s => new Article()
                    {
                        Id = s.Id,
                        CreateTime = s.CreateTime,
                        Title = s.Title
                    })
                    .OrderBy(s => Guid.NewGuid())
                    .Take(8)
                    .ToListAsync();

                articleListModel.Categorys = await _dbContext.ArticleCategory
                    .Where(s => s.Status == (int)CommonStatus.Valid)
                    .ToListAsync();

                //文章总数
                int totalCount = await _dbContext.Article.Where(s => s.Status == (int)CommonStatus.Valid).CountAsync();
                //分页参数
                int totalPage = (totalCount + pageSize - 1) / pageSize;

                if (pageIndex > totalPage)
                    return new NotFoundViewResult();

                ViewData["pageParam"] = new PageParam(pageIndex, pageSize, totalCount, totalPage);

                return View(articleListModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 文章分类列表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [Route("Article/Cate/{categoryId?}/{pageIndex?}")]
        public async Task<IActionResult> Cate(int categoryId, int pageIndex = 0)
        {
            if (categoryId <= 0)
                return new NotFoundViewResult();
            try
            {
                int pageSize = 10;

                ArticleListModel articleListModel = new ArticleListModel();

                articleListModel.Articles = await _dbContext.Article
                    .Where(s => s.Status == (int)CommonStatus.Valid && s.CategoryId == categoryId)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .Include(s => s.Category)
                    .OrderByDescending(s => s.CreateTime)
                    .ToListAsync();

                articleListModel.RecommendArticles = await _dbContext.Article
                    .Where(s => s.IsRecommend && s.Status == (int)CommonStatus.Valid)
                     .Select(s => new Article()
                     {
                         Id = s.Id,
                         CreateTime = s.CreateTime,
                         Title = s.Title
                     })
                    .OrderByDescending(s => s.CreateTime)
                    .ToListAsync();

                articleListModel.RandomArticles = await _dbContext.Article
                    .Where(s => s.Status == (int)CommonStatus.Valid)
                    .Select(s => new Article()
                    {
                        Id = s.Id,
                        CreateTime = s.CreateTime,
                        Title = s.Title
                    })
                    .OrderBy(s => Guid.NewGuid())
                    .Take(8)
                    .ToListAsync();

                articleListModel.Categorys = await _dbContext.ArticleCategory.Where(s => s.Status == (int)CommonStatus.Valid).ToListAsync();

                //文章总数
                int totalCount = await _dbContext.Article.Where(s => s.Status == (int)CommonStatus.Valid && s.CategoryId == categoryId).CountAsync();
                //分页参数
                int totalPage = (totalCount + pageSize - 1) / pageSize;

                if (pageIndex > totalPage)
                    return new NotFoundViewResult();

                ViewData["pageParam"] = new PageParam(pageIndex, pageSize, totalCount, totalPage);
                ViewData["categoryId"] = categoryId;

                return View("All", articleListModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 文章详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0)
                return new NotFoundViewResult();

            Article article = await _dbContext.Article.Where(s => s.Status == (int)CommonStatus.Valid)
                .Include(s => s.Remarks).ThenInclude(s => s.User)
                .Include(s => s.Remarks).ThenInclude(s => s.RemarkReplys).ThenInclude(s => s.User)
                .Include(s => s.Remarks).ThenInclude(s => s.RemarkReplys).ThenInclude(s => s.TargetUser)
                .Include(s => s.ArticleMusics).ThenInclude(s => s.Music)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (article == null)
                return new NotFoundViewResult();

            ArticleDetailModel detailModel = new ArticleDetailModel();

            detailModel.Article = article;

            detailModel.ArticleCategorys = await _dbContext.Set<ArticleCategory>().ToListAsync();

            detailModel.RandomArticles = await _dbContext.Article
                .Where(s => s.Status == (int)CommonStatus.Valid)
                .Select(s => new Article()
                {
                    Id = s.Id,
                    CreateTime = s.CreateTime,
                    Title = s.Title
                })
                .OrderBy(s => Guid.NewGuid())
                .Take(8)
                .ToListAsync();

            var similarArticles = await _dbContext.Article
                .Where(s => s.Status == (int)CommonStatus.Valid && s.Id != article.Id)
                .Select(s => new Article()
                {
                    Id = s.Id,
                    CreateTime = s.CreateTime,
                    Title = s.Title
                })
                .ToListAsync();
            //筛选相似数据
            similarArticles = similarArticles.Where(s => StringSimilarity.Calculate(article.Title, s.Title) > 0.3m).OrderBy(s => Guid.NewGuid()).Take(8).ToList();

            detailModel.SimilarArticles = similarArticles;

            string viewCountStr = await _cache.GetStringAsync($"article{id}");
            if (viewCountStr.IsNullOrEmpty())
                viewCountStr = "0";
            int viewCount = int.Parse(viewCountStr) + 1;
            await _cache.SetStringAsync($"article{id}", viewCount.ToString());

            article.ReadCount += viewCount;
            if (viewCount == int.Parse(Configuration["AppSettings:Threshold"]))
            {
                _dbContext.Set<Article>().Update(article);
                if (await _dbContext.SaveChangesAsync() <= 0)
                    _logger.LogWarning($"更新文章浏览量失败，ID：{article.Id}");
                await _cache.RemoveAsync($"article{id}");
            }

            return View(detailModel);
        }
    }
}
