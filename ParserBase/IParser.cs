using AngleSharp.Html.Dom;
namespace ParserBase
{   /// <summary>
    /// Класс реализующие этот интерфейс смогут возвращаться данные любого ссылочного типа.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IParser<T> where T : class
    {   /// <summary>
        /// тип T при реализации будет заменяться на любой другой тип.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        T Parse(IHtmlDocument document);
    }
}
