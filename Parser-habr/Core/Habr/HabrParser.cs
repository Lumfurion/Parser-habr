using AngleSharp.Html.Dom;
using ParserBase;
using System.Collections.Generic;


namespace Parser_habr
{
    class HabrData
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
       
        public HabrData(string title, string text, string image)
        {
            Title = title;
            Text = text;
            Image = image;
        }
        
    }
    /// <summary>
    ///Реализуем сам интерфейс для парсинга сайта  https://habr.com/ru/all ,
    ///данный класс будет извлекать данные из habr.com и возвращать.
    /// </summary>
    class HabrParser : IParser<HabrData[]>
    {
        public HabrData[] Parse(IHtmlDocument document)
        {
            var list = new List<HabrData>();
            var news = document.QuerySelectorAll("article.post");
            foreach (var item in news)
            {
                var content = item.QuerySelector("div.post__text");

                var content_title = item.QuerySelector("h2.post__title").TextContent.Trim();
                var content_image = content.QuerySelector("img")?.GetAttribute("src");
                var content_text = content.TextContent.Trim();
                list.Add(new HabrData(content_title, content_text,content_image));
            }

            return list.ToArray();
        }
    }
}
