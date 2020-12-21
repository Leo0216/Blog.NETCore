/*---------------------------------------------------------------------------------- 
 * 类 名 称：ExtCache
 * 类 描 述：WebAPI返回结果Model类 带URL
 * 作    者：Leo
 * 创建时间：2019/9/25 18:13:11
 * 更新时间：2019/9/25 18:13:11
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
namespace Leo.Framework.Models.WebAPI
{
    public class ApiResultWithUrl : ApiResult
    {
        public ApiResultWithUrl(int code = -1, string msg = "操作失败", string redirect = null) : base(code, msg)
        {
            Redirect = redirect;
        }
        /// <summary>
        /// 重定向地址
        /// </summary>
        public string Redirect { get; set; }
    }
}
