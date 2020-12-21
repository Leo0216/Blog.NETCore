/*---------------------------------------------------------------------------------- 
 * 类 名 称：ArticleCategory
 * 类 描 述：文章类别类
 * 作    者：Leo
 * 创建时间：2019/9/25 17:05:59
 * 更新时间：2019/9/25 17:05:59
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.EntityFramework
{
    public class ArticleCategory : EntityPlus
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int Order { get; set; }

        #region 导航属性
        public virtual ICollection<Article> Articles { get; set; }
        #endregion
    }
}
