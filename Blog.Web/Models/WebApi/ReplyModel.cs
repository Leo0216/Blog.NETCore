namespace Blog.Web.Models.WebApi
{
    public class ReplyModel
    {
        public int ArticleId { get; set; }
        public int RemarkId { get; set; }
        public int TargetUserId { get; set; }
        public string ReplyContent { get; set; }
    }
}
