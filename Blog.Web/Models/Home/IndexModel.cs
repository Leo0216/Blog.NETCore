using Blog.EntityFramework;
using System.Collections.Generic;

namespace Blog.Web.Models
{
    public class IndexModel
    {
        /// <summary>
        /// 公告
        /// </summary>
        public List<Announcement> Announcements { get; set; }
        /// <summary>
        /// 轮播
        /// </summary>
        public List<Carousel> Carousels { get; set; }
        /// <summary>
        /// 最新博文
        /// </summary>
        public List<Article> Articles { get; set; }
        /// <summary>
        /// 热门博文
        /// </summary>
        public List<Article> HotArticles { get; set; }
        /// <summary>
        /// 推荐博文
        /// </summary>
        public List<Article> RecommendArticles { get; set; }
        /// <summary>
        /// 置顶博文
        /// </summary>
        public List<Article> SetTopArticles { get; set; }
        /// <summary>
        /// 最新评论
        /// </summary>
        public List<Remark> NewRemarks { get; set; }
        /// <summary>
        /// 最新留言
        /// </summary>
        public List<Comment> NewComments { get; set; }
        /// <summary>
        /// 友情链接
        /// </summary>
        public List<FriendLink> FriendLinks { get; set; }
        /// <summary>
        /// 文章总数
        /// </summary>
        public int ArticleCount { get; set; }
        /// <summary>
        /// 细语总数
        /// </summary>
        public int TimelineCount { get; set; }
        /// <summary>
        /// 评论总数
        /// </summary>
        public int RemarkCount { get; set; }
        /// <summary>
        /// 留言总数
        /// </summary>
        public int CommentCount { get; set; }
        

    }
}
