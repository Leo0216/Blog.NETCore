/*---------------------------------------------------------------------------------- 
 * 类 名 称：FriendLink
 * 类 描 述：友情链接类
 * 作    者：Leo
 * 创建时间：2019/9/25 17:10:35
 * 更新时间：2019/9/25 17:10:35
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.EntityFramework
{
    public class FriendLink : EntityPlus
    {
        /// <summary>
        /// 网站名称
        /// </summary>
        [StringLength(10)]
        public string Title { get; set; }
        /// <summary>
        /// 网站链接
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string Link { get; set; }
        /// <summary>
        /// 网站域名
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string Domain { get; set; }
        /// <summary>
        /// 网站图标
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string Favicon { get; set; }
    }
}
