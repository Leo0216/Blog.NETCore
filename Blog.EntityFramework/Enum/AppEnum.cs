/*---------------------------------------------------------------------------------- 
 * 类 名 称：AppEnum
 * 类 描 述：程序所有枚举
 * 作    者：Leo
 * 创建时间：2019/9/25 17:01:26
 * 更新时间：2019/9/25 17:01:26
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/

namespace Blog.EntityFramework.Enum
{
    /// <summary>
    /// 用户消息类型
    /// </summary>
    public enum UserMessageType
    {
        /// <summary>
        /// 评论
        /// </summary>
        Remark = 1,
        /// <summary>
        /// 留言
        /// </summary>
        Comment = 2,
        /// <summary>
        /// 系统
        /// </summary>
        System = 99
    }

    /// <summary>
    /// 用户消息状态
    /// </summary>
    public enum UserMessageStatus
    {
        /// <summary>
        /// 作废
        /// </summary>
        Invalid = -1,
        /// <summary>
        /// 未读
        /// </summary>
        Unread = 0,
        /// <summary>
        /// 已读
        /// </summary>
        Read = 1,
        /// <summary>
        /// 已处理
        /// </summary>
        Dealt = 2
    }

    /// <summary>
    /// 文章状态枚举
    /// </summary>
    public enum CommonStatus
    {
        /// <summary>
        /// 作废
        /// </summary>
        Invalid = -1,
        /// <summary>
        /// 草稿
        /// </summary>
        Draft = 0,
        /// <summary>
        /// 有效
        /// </summary>
        Valid = 1
    }

    public enum MenuType
    {
        /// <summary>
        /// 目录
        /// </summary>
        Catalogue = 1,
        /// <summary>
        /// 按钮
        /// </summary>
        Button = 2
    }

    public enum Target
    {
        _self = 0,
        _blank = 1,
        _parent = 2,
        _top = 3
    }
}
