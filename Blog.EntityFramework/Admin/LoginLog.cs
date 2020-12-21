/*---------------------------------------------------------------------------------- 
 * 类 名 称：LoginLog
 * 类 描 述：登录日志类，后台系统管理员的登录日志
 * 作    者：Leo
 * 创建时间：2019/9/25 16:55:37
 * 更新时间：2019/9/25 16:55:37
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.EntityFramework.Admin
{
    public class LoginLog : Entity
    {
        /// <summary>
        /// 登录IP
        /// </summary>
        [StringLength(20)]
        public string IP { get; set; }
        /// <summary>
        /// 登录远程IP信息
        /// </summary>
        [StringLength(50)]
        public string RemoteIPInfo { get; set; }
        /// <summary>
        /// 登陆时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 客户端代理
        /// </summary>
        [StringLength(200)]
        public string UserAgent { get; set; }

        #region 外键
        public int AdministratorId { get; set; }
        #endregion

        #region 导航属性
        public virtual Administrator Administrator { get; set; }
        #endregion
    }
}
