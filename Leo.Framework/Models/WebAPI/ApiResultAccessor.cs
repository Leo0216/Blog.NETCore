/*---------------------------------------------------------------------------------- 
 * 类 名 称：ExtCache
 * 类 描 述：WebAPI返回结果类访问器
 * 作    者：Leo
 * 创建时间：2019/9/25 18:13:11
 * 更新时间：2019/9/25 18:13:11
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leo.Framework.Models.WebAPI
{
    public class ApiResultAccessor
    {
        private static ApiResult _unauthorized;
        private static ApiResult _noPermission;
        private static ApiResult _unsuccessful;
        private static ApiResult _successfully;
        private static ApiResult _exception;

        /// <summary>
        /// API返回结果：无权访问
        /// </summary>
        public static ApiResult Unauthorized
        {
            get
            {
                if (_unauthorized == null)
                {
                    _unauthorized = new ApiResult(-1, "无权访问");
                }
                return _unauthorized;
            }
        }
        /// <summary>
        /// API返回结果：用户权限不足
        /// </summary>
        public static ApiResult NoPermission
        {
            get
            {
                if (_noPermission == null)
                {
                    _noPermission = new ApiResult(0, "当前用户没有这项操作的权限");
                }
                return _noPermission;
            }
        }
        /// <summary>
        /// API返回结果：失败
        /// </summary>
        public static ApiResult Unsuccessful
        {
            get
            {
                if (_unsuccessful == null)
                {
                    _unsuccessful = new ApiResult();
                }
                return _unsuccessful;
            }
        }
        /// <summary>
        /// API返回结果：成功
        /// </summary>
        public static ApiResult Successfully
        {
            get
            {
                if (_successfully == null)
                {
                    _successfully = new ApiResult(1, "操作成功");
                }
                return _successfully;
            }
        }

        public static ApiResult Exception
        {
            get
            {
                if (_exception == null)
                {
                    _exception = new ApiResult(-1, "程序异常");
                }
                return _exception;
            }
        }
    }
}
