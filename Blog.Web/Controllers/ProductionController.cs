using Blog.EntityFramework;
using Blog.EntityFramework.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public class ProductionController : Controller
    {
        private DataContext _dbContext;
        public ProductionController(DataContext dataContext)
        {
            _dbContext = dataContext;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewData["currentPage"] = "Production";
            base.OnActionExecuted(context);
        }

        public async Task<IActionResult> Index()
        {
            var productions = await _dbContext.Production.Where(s => s.Status == (int)CommonStatus.Valid).OrderByDescending(s => s.CreateTime).Take(30).ToListAsync();
            return View(productions);
        }
    }
}