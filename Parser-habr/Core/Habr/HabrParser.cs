using System.Collections.Generic;
using AngleSharp.Html.Dom;
using Parser_habr.Models;
using ParserBase;
namespace Parser_habr
{
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
