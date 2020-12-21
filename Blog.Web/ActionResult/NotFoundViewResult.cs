using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blog.Web
{
    public class NotFoundViewResult : ViewResult
    {
        /// <summary>
        /// 返回404状态码页面
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="tips">提示语</param>
        public NotFoundViewResult(string viewName = "Error404")
        {
            ViewName = viewName;
            StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}
