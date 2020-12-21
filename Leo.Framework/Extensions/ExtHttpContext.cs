/*---------------------------------------------------------------------------------- 
 * 类 名 称：ExtCache
 * 类 描 述：HttpContext扩展方法
 * 作    者：Leo
 * 创建时间：2019/9/25 18:04:33
 * 更新时间：2019/9/25 18:04:33
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text;

namespace Leo.Framework.Extensions
{
    public static class ExtHttpContext
    {
        public static string GetUserIP(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }

        /// <summary>
        /// 获取当前请求的完整路径
        /// </summary>
        /// <param name="request">HttpRequest对象</param>
        /// <returns>当前请求的完整路径</returns>
        public static string GetAbsoluteUri(this HttpRequest request)
        {
            return new StringBuilder()
                .Append(request.Scheme)
                .Append("://")
                .Append(request.Host)
                .Append(request.PathBase)
                .Append(request.Path)
                .Append(request.QueryString)
                .ToString();
        }

    }
}
