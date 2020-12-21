/*---------------------------------------------------------------------------------- 
 * 类 名 称：RemarkReply
 * 类 描 述：评论回复类
 * 作    者：Leo
 * 创建时间：2019/9/25 17:14:21
 * 更新时间：2019/9/25 17:14:21
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.EntityFramework
{
    public class RemarkReply : EntityPlus
    {
        [StringLength(500)]
        public string Content { get; set; }

        #region 外键
        /// <summary>
        /// 评论Id
        /// </summary>
        public int RemarkId { get; set; }
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
        [ForeignKey("RemarkId")]
        public virtual Remark Remark { get; set; }
        #endregion
    }
}
