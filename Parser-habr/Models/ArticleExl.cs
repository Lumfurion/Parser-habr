using System;
using System.Collections.Generic;

namespace Parser_habr.Models
{
    class ArticleExl
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public ArticleExl() { }

     
        public ArticleExl(string title, string text, string image)
        {
            Title = title;
            Text = text;
            Image = image;
        }

       
    }
}
