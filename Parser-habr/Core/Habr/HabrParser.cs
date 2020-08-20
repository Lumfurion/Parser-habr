using System.Collections.Generic;
using System.Xml.Serialization;
using AngleSharp.Html.Dom;
using ParserBase;


namespace Parser_habr
{
    [XmlRoot(ElementName = "article")]
    public class Article
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "text")]
        public string Text { get; set; }
        [XmlElement(ElementName = "image")]
        public string Image { get; set; }

        public Article(){ }
        public Article(string title, string text, string image)
        {
            Title = title;
            Text = text;
            Image = image;
        }
    }
    [XmlRoot(ElementName = "articles")]
    public class Articles
    {
        [XmlElement(ElementName = "article")]
        public List<Article> Articleslist { get; set; }
        public Articles(){}
      
        public static Articles Craete(List<Article> articles)
        {
            Articles art = new Articles();
            art.Articleslist = articles;
            return art;
        }
    }

    /// <summary>
    ///Реализуем сам интерфейс для парсинга сайта  https://habr.com/ru/all ,
    ///данный класс будет извлекать данные из habr.com и возвращать.
    /// </summary>
    class HabrParser : IParser<Article[]>
    {
        public Article[] Parse(IHtmlDocument document)
        {
            var list = new List<Article>();
            var news = document.QuerySelectorAll("article.post");
            foreach (var item in news)
            {
                var content = item.QuerySelector("div.post__text");

                var content_title = item.QuerySelector("h2.post__title").TextContent.Trim();
                var content_image = content.QuerySelector("img")?.GetAttribute("src");
                var content_text = content.TextContent.Trim();
                list.Add(new Article(content_title, content_text,content_image));
            }

            return list.ToArray();
        }
    }
}
