/*---------------------------------------------------------------------------------- 
 * 类 名 称：Note
 * 类 描 述：笔记类
 * 作    者：Leo
 * 创建时间：2019/9/25 17:12:38
 * 更新时间：2019/9/25 17:12:38
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System.ComponentModel.DataAnnotations;

namespace Blog.EntityFramework
{
    public class Note : EntityPlus
    {
        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(50)]
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [StringLength(1000)]
        public string Content { get; set; }

        /// <summary>
        /// 标签 逗号隔开
        /// </summary>
        [StringLength(100)]
        public string Tag { get; set; }
    }
}
