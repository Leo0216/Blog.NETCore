/*---------------------------------------------------------------------------------- 
 * 类 名 称：User
 * 类 描 述：用户类
 * 作    者：Leo
 * 创建时间：2019/9/25 17:16:08
 * 更新时间：2019/9/25 17:16:08
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.EntityFramework
{
    public class User : EntityPlus
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(100)]
        public string Avatar { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [StringLength(200)]
        public string Sign { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [StringLength(20)]
        public string City { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(20)]
        public string Remark { get; set; }

        /// <summary>
        /// QQOpenId
        /// </summary>
        public string QQOpenId { get; set; }

        /// <summary>
        /// 上次登录IP
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string LastLoginIP { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        #region 导航属性
        public virtual ICollection<UserMessage> UserMessages { get; set; }
        #endregion
    }
}
