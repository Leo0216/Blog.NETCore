/*---------------------------------------------------------------------------------- 
 * 类 名 称：Announcement
 * 类 描 述：网站公告类
 * 作    者：Leo
 * 创建时间：2019/9/25 17:04:19
 * 更新时间：2019/9/25 17:04:19
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System.ComponentModel.DataAnnotations;

namespace Blog.EntityFramework
{
    public class Announcement : EntityPlus
    {
        /// <summary>
        /// 内容
        /// </summary>
        [StringLength(100)]
        public string Content { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }
    }
}
