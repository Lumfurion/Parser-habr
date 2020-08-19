using ParserBase;
namespace Parser_habr
{   /// <summary>
    ///В классе реализуем интерфейс для настройки парсера.
    ///То есть границы парсера StartPoint,EndPoint и сайт BaseUrl а также какая это страница Postfix.
    /// </summary>
    class HabrSettings : IParserSettings
    {   /// <summary>
        ///Здесь прописываем url сайта. 
        /// </summary>
        public string BaseUrl { get; set; } = "https://habr.com/ru/all";
        /// <summary>
        /// вместо CurrentID будет подставляться номер страницы.
        /// </summary>
        public string Postfix { get; set; } = "page{CurrentId}"; 
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
       
        public HabrSettings(int start, int end)
        {
            StartPoint = start;
            EndPoint = end;
        }
    }
}
