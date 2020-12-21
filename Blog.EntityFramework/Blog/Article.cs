/*---------------------------------------------------------------------------------- 
 * 类 名 称：Article
 * 类 描 述：文章类
 * 作    者：Leo
 * 创建时间：2019/9/25 17:05:20
 * 更新时间：2019/9/25 17:05:20
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
    public class Article : EntityPlus
    {
        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(50)]
        public string Title { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        [StringLength(500)]
        public string Abstract { get; set; }
        /// <summary>
        /// SEO关键词
        /// </summary>
        [StringLength(100)]
        public string Keywords { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        [StringLength(100)]
        public string Cover { get; set; }
        /// <summary>
        /// 阅读量
        /// </summary>
        public int ReadCount { get; set; }
        /// <summary>
        /// 评论量
        /// </summary>
        public int RemarkCount { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsRecommend { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }
        /// <summary>
        /// 是否允许评论
        /// </summary>
        public bool AllowRemark { get; set; }
        /// <summary>
        /// 是否允许回复
        /// </summary>
        public bool AllowReply { get; set; }
        /// <summary>
        /// 点赞数量
        /// </summary>
        public int PraiseCount { get; set; }
        /// <summary>
        /// 分享数量
        /// </summary>
        public int ShareCount { get; set; }

        #region 外键
        public int CategoryId { get; set; }
        public int? AttachmentId { get; set; }
        #endregion

        #region 导航属性
        [ForeignKey("CategoryId")]
        public virtual ArticleCategory Category { get; set; }
        [ForeignKey("AttachmentId")]
        public virtual Attachment Attachment { get; set; }

        public virtual ICollection<Remark> Remarks { get; set; }
        public virtual ICollection<ArticleMusic> ArticleMusics { get; set; }
        #endregion
    }
}
