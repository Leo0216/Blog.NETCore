/*---------------------------------------------------------------------------------- 
 * 类 名 称：ExtCache
 * 类 描 述：WebAPI返回结果Model类
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
    public class ApiResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="msg">返回消息</param>
        public ApiResult(int code = -1, string msg = "操作失败")
        {
            Code = code;
            Msg = msg;
        }

        /// <summary>
        /// 返回码 1为成功 非1为失败
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Msg { get; set; }
    }
}
