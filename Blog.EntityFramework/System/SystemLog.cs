/*---------------------------------------------------------------------------------- 
 * 类 名 称：SystemLog
 * 类 描 述：系统日志类，前台和后台的日志信息
 * 作    者：Leo
 * 创建时间：2019/9/25 17:02:43
 * 更新时间：2019/9/25 17:02:43
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.EntityFramework
{
    public class SystemLog : Entity
    {
        /// <summary>
        /// 日志记录时间
        /// </summary>
        public DateTime LogDate { get; set; }
        /// <summary>
        /// 日志等级
        /// </summary>
        [StringLength(10)]
        public string Level { get; set; }
        /// <summary>
        /// 日志消息
        /// </summary>
        [StringLength(300)]
        public string Message { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        [StringLength(1000)]
        public string Exception { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        [StringLength(1000)]
        public string CallSite { get; set; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string Controller { get; set; }
        /// <summary>
        /// 动作方法名称
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string Action { get; set; }
        /// <summary>
        /// 客户端代理信息
        /// </summary>
        [Column(TypeName = "varchar(300)")]
        public string UserAgent { get; set; }
        /// <summary>
        /// 请求IP
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string IP { get; set; }
        /// <summary>
        /// 请求路径
        /// </summary>
        [Column(TypeName = "varchar(300)")]
        public string Url { get; set; }
        /// <summary>
        /// 请求来源
        /// </summary>
        [Column(TypeName = "varchar(300)")]
        public string Referrer { get; set; }

        /// <summary>
        /// 当前登录用户ID
        /// </summary>
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
