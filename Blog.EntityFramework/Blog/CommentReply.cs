/*---------------------------------------------------------------------------------- 
 * 类 名 称：CommentReply
 * 类 描 述：留言回复类
 * 作    者：Leo
 * 创建时间：2019/9/25 17:10:02
 * 更新时间：2019/9/25 17:10:02
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.EntityFramework
{
    public class CommentReply : EntityPlus
    {
        [StringLength(500)]
        public string Content { get; set; }

        #region 外键
        /// <summary>
        /// 留言Id
        /// </summary>
        public int CommentId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 目标用户Id
        /// </summary>
        public int TargetUserId { get; set; }
        #endregion

        #region 导航属性
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("TargetUserId")]
        public virtual User TargetUser { get; set; }
        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }
        #endregion
    }
}
