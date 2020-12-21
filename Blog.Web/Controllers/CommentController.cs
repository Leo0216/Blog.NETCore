using Blog.EntityFramework;
using Blog.EntityFramework.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public class CommentController : Controller
    {
        private DataContext _dbContext;
        public CommentController(DataContext dataContext)
        {
            _dbContext = dataContext;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewData["currentPage"] = "Comment";
            base.OnActionExecuted(context);
        }
        public async Task<IActionResult> Index([FromServices]IConfiguration configuration)
        {
            var comments = await _dbContext.Comment
                .Include(s => s.User)
                .Include(s => s.CommentReplys).ThenInclude(s => s.TargetUser)
                .Include(s => s.CommentReplys).ThenInclude(s => s.User)
                .Where(s => s.Status == (int)CommonStatus.Valid)
                .OrderByDescending(s => s.CreateTime)
                .Take(10)
                .ToListAsync();
            if (configuration["AppSettings:CloseComment"] != null && bool.Parse(configuration["AppSettings:CloseComment"]))
                return View("Blank", comments);
            return View(comments);
        }
    }
}