using Blog.EntityFramework;
using Blog.EntityFramework.Enum;
using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public class AboutController : Controller
    {
        private readonly DataContext _dbContext;
        public AboutController(DataContext dataContext)
        {
            _dbContext = dataContext;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewData["currentPage"] = "About";
            base.OnActionExecuted(context);
        }

        public async Task<IActionResult> Index()
        {
            AboutModel aboutModel = new AboutModel();
            aboutModel.FriendLinks = await _dbContext.FriendLink.Where(s => s.Status == (int)CommonStatus.Valid).ToListAsync();
            return View(aboutModel);
        }
    }
}