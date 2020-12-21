using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Leo.Framework.Extensions;

namespace Blog.Web.ViewComponents
{
    public class PageNav : ViewComponent
    {
        public IViewComponentResult Invoke(PageParam pageParam, string urlFormat)
        {
            if (urlFormat.IsNullOrEmpty())
                throw new System.ArgumentNullException("urlFormat");

            ViewData["params"] = pageParam;
            ViewData["urlFormat"] = urlFormat;
            return View();
        }
    }
}
