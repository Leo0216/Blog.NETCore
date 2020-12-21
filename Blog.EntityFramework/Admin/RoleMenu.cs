/*---------------------------------------------------------------------------------- 
 * 类 名 称：RoleMenu
 * 类 描 述：角色权限类，关联角色和权限
 * 作    者：Leo
 * 创建时间：2019/9/25 16:59:46
 * 更新时间：2019/9/25 16:59:46
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/

namespace Blog.EntityFramework.Admin
{
    public class RoleMenu
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
