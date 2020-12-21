/*---------------------------------------------------------------------------------- 
 * 类 名 称：Role
 * 类 描 述：角色类，后台系统管理员角色
 * 作    者：Leo
 * 创建时间：2019/9/25 16:58:37
 * 更新时间：2019/9/25 16:58:37
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.EntityFramework.Admin
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : Entity
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [StringLength(10)]
        public string Name { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        [StringLength(100)]
        public string Description { get; set; }
        /// <summary>
        /// 是否超级管理员（全权）
        /// </summary>
        public bool SuperAdministrator { get; set; }
        /// <summary>
        /// 数据状态
        /// </summary>
        public int Status { get; set; }

        #region 导航属性
        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
        public virtual ICollection<Administrator> Administrators { get; set; }
        #endregion
    }
}
