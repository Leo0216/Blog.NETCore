/*---------------------------------------------------------------------------------- 
 * 类 名 称：Menu
 * 类 描 述：菜单类，后台系统菜单
 * 作    者：Leo
 * 创建时间：2019/9/25 16:57:56
 * 更新时间：2019/9/25 16:57:56
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using Blog.EntityFramework.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.EntityFramework.Admin
{
    public class Menu : EntityPlus
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        [StringLength(10)]
        public string Name { get; set; }
        /// <summary>
        /// 菜单URL
        /// </summary>
        [StringLength(50)]
        public string Url { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        [StringLength(100)]
        public string Icon { get; set; }
        /// <summary>
        /// 菜单类型
        /// </summary>
        public MenuType Type { get; set; }
        /// <summary>
        /// 菜单描述
        /// </summary>
        [StringLength(100)]
        public string Description { get; set; }
        /// <summary>
        /// 菜单排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public int ParentId { get; set; }

        [NotMapped]
        public string ParentUrl { get; set; }
        [NotMapped]
        public virtual IList<Menu> Children { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        [NotMapped]
        public bool Checked { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        [NotMapped]
        public bool Spread { get; set; }
        /// <summary>
        /// 节点标题
        /// </summary>
        [NotMapped]
        public string Title { get { return Name; } }
        #region 导航属性
        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
        #endregion
    }
}
