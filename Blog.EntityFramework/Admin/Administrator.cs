/*---------------------------------------------------------------------------------- 
 * 类 名 称：Administrator
 * 类 描 述：管理员类，后台系统管理员
 * 作    者：Leo
 * 创建时间：2019/9/25 16:54:54
 * 更新时间：2019/9/25 16:54:54
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.EntityFramework.Admin
{
    /// <summary>
    /// 管理员类
    /// </summary>
    public class Administrator : EntityPlus
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        #region 外键
        public int RoleId { get; set; }
        #endregion

        #region 导航属性
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        public virtual ICollection<LoginLog> LoginLogs { get; set; }
        #endregion
    }
}
