using Blog.EntityFramework;
using System.Collections.Generic;

namespace Blog.Web.Models
{
    public class ArticleDetailModel
    {
        public Article Article { get; set; }
        public List<ArticleCategory> ArticleCategorys { get; set; }
        public List<Article> RandomArticles { get; set; }
        public List<Article> SimilarArticles { get; set; }
    }
}
