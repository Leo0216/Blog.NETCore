/*---------------------------------------------------------------------------------- 
 * 类 名 称：Comment
 * 类 描 述：留言类
 * 作    者：Leo
 * 创建时间：2019/9/25 17:09:07
 * 更新时间：2019/9/25 17:09:07
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
    public class Comment : EntityPlus
    {
        [StringLength(500)]
        public string Content { get; set; }

        #region 外键
        public int UserId { get; set; }
        #endregion

        #region 导航属性
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        /// <summary>
        /// 回复集合
        /// </summary>
        public virtual ICollection<CommentReply> CommentReplys { get; set; }
        #endregion
    }
}
