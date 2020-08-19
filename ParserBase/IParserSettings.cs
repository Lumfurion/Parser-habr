namespace ParserBase
{    /// <summary>
     /// Настройка парсера. 
     /// </summary>
    public interface IParserSettings
    {   /// <summary>
        ///url сайта
        /// </summary>
        string BaseUrl { get; set; }
        /// <summary>
        /// в постфикс будет передаваться id страницы
        /// </summary>
        string Postfix { get; set; }
        /// <summary>
        /// c какой страницы парсим данные
        /// </summary>
        int StartPoint { get; set; }
        /// <summary>
        /// по какую страницу парсим данные
        /// </summary>
        int EndPoint { get; set; }
    }
}
