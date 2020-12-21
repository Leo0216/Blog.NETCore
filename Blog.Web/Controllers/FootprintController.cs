using Blog.EntityFramework;
using Blog.EntityFramework.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public class FootprintController : Controller
    {
        private DataContext _dbContext;
        public FootprintController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewData["currentPage"] = "Footprint";
            base.OnActionExecuted(context);
        }
        public async Task<IActionResult> Timeline()
        {
            var timeline = await _dbContext.Timeline.Where(s => s.Status != (int)CommonStatus.Invalid).ToListAsync();
            return View(timeline);
        }

        public async Task<IActionResult> ArticleRecord()
        {
            var data = await _dbContext.Article.Where(s => s.Status == (int)CommonStatus.Valid).Select(s => new Article
            {
                Id = s.Id,
                Title = s.Title,
                CreateTime = s.CreateTime
            }).ToListAsync();

            var group = data.GroupBy(s => s.CreateTime.Year).ToList();

            return View(group);
        }

        public IActionResult Note()
        {
            return View();
        }
    }
}