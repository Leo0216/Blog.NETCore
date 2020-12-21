/*---------------------------------------------------------------------------------- 
 * 类 名 称：Remark
 * 类 描 述：评论类
 * 作    者：Leo
 * 创建时间：2019/9/25 17:13:50
 * 更新时间：2019/9/25 17:13:50
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
    public class Remark : EntityPlus
    {
        /// <summary>
        /// 评论内容
        /// </summary>
        [StringLength(500)]
        public string Content { get; set; }

        #region 外键
        /// <summary>
        /// 评论文章Id
        /// </summary>
        public int ArticleId { get; set; }
        /// <summary>
        /// 评论者Id
        /// </summary>
        public int UserId { get; set; }
        #endregion

        #region 导航属性
        /// <summary>
        /// 评论者
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        /// <summary>
        /// 评论回复集合
        /// </summary>
        public virtual ICollection<RemarkReply> RemarkReplys { get; set; }
        #endregion
    }
}
