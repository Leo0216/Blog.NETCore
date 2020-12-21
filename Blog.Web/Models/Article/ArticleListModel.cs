using Blog.EntityFramework;
using System.Collections.Generic;

namespace Blog.Web.Models
{
    public class ArticleListModel
    {
        public List<Article> Articles { get; set; }
        public List<Article> RecommendArticles { get; set; }
        public List<Article> RandomArticles { get; set; }

        public List<ArticleCategory> Categorys { get; set; }
    }
}