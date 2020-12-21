/*---------------------------------------------------------------------------------- 
 * 类 名 称：UserMessage
 * 类 描 述：用户消息类
 * 作    者：Leo
 * 创建时间：2019/9/25 17:16:43
 * 更新时间：2019/9/25 17:16:43
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.EntityFramework
{
    public class UserMessage : EntityPlus
    {
        public UserMessage() { }
        /// <summary>
        /// 创建用户消息实例
        /// </summary>
        /// <param name="targetUserId">消息目标用户</param>
        /// <param name="content">消息内容</param>
        /// <param name="messageType">消息类型 枚举 默认 99 系统提醒</param>
        /// <param name="status">消息状态 枚举 默认0 未读</param>
        public UserMessage(int targetUserId, string content, int messageType = 99, int status = 0)
        {
            TargetUserId = targetUserId;
            Content = content;
            MessageType = messageType;
            Status = status;
            CreateTime = DateTime.Now;
        }
        /// <summary>
        /// 消息内容
        /// </summary>
        [StringLength(500)]
        public string Content { get; set; }
        /// <summary>
        /// 消息类型 1 评论 2 留言  99系统通知
        /// </summary>
        public int MessageType { get; set; }
        /// <summary>
        /// 目标用户
        /// </summary>
        public int TargetUserId { get; set; }

        #region 导航属性
        [ForeignKey("TargetUserId")]
        public virtual User TargetUser { get; set; }
        #endregion
    }
}
