/*---------------------------------------------------------------------------------- 
 * 类 名 称：Attachment
 * 类 描 述：附件类，文章的附件
 * 作    者：Leo
 * 创建时间：2019/9/25 17:07:04
 * 更新时间：2019/9/25 17:07:04
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.EntityFramework
{
    public class Attachment : EntityPlus
    {
        /// <summary>
        /// 附件名称
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// 附件链接
        /// </summary>
        [Column(TypeName = "varchar(300)")]
        public string Link { get; set; }
        /// <summary>
        /// 是否允许下载
        /// </summary>
        public bool AllowDownload { get; set; }

        #region 导航属性
        public virtual ICollection<Article> Articles { get; set; }
        #endregion
    }
}
