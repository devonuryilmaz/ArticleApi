using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Article
    {

        public Article()
        {
            this.ArticleAddedDate = this.ArticleAddedDate <= DateTime.MinValue ? DateTime.Now : this.ArticleAddedDate;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortArticleContent { get; set; }
        public string FullArticleContent { get; set; }

        public DateTime ArticleAddedDate { get; set; }
    }
}
