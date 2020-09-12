using System;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
namespace ParserBase
{   /// <summary>
    /// Создав 1 раз ParserWorker не когда больше туда не залазить,он может работать любыми данными и  любыми парсирами.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ParserWorker<T> where T : class
    {
        #region Cвойства.
        T items { get; set; }
        public T Items
        {
            get { return items; }
        }

        /// <summary>
        /// Парсер.
        /// </summary>
        IParser<T> parser;
        public IParser<T> Parser
        {
            get { return parser; }
            set { parser = value; }
        }
        
        /// <summary>
        ///Загрузчик кода страницы.
        /// </summary>
        HtmlLoader loader;

        /// <summary>
        /// Настройки для загрузчика кода страниц.
        /// </summary>
        IParserSettings parserSettings;
        public IParserSettings Settings
        {
            get { return parserSettings; }
            set
            {
                parserSettings = value; //Новые настройки парсера
                loader = new HtmlLoader(value); //сюда помещаются настройки для загрузчика кода страницы
            }
        }

        /// <summary>
        /// Проверяем активность парсера.
        /// </summary>
        bool isActive;
        public bool IsActive
        {
            get { return isActive; }
        }

        /// <summary>
        /// Это событие возвращает спаршенные за итерацию данные(первый аргумент ссылка на парсер, и сами данные вторым аргументом)
        /// </summary>
        public event Action<object, T> OnNewData;
        /// <summary>
        /// Это событие отвечает информирование при завершении работы парсера.
        /// </summary>
        public event Action<object> OnComplited;
        #endregion

        /// <summary>
        /// 1-й конструктор, в качестве аргумента будет передеваться класс реализующий интерфейс IParser.
        /// </summary>
        /// <param name="parser"></param>
        public ParserWorker(IParser<T> parser)
        {
            Parser = parser;
        }
        public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser)
        {
            Settings = parserSettings;
        }

        public void Start() //Запускаем парсер
        {
            isActive = true;
            Worker();
        }

        public void Stop() //Останавливаем парсер
        {
            isActive = false;
        }
        /// <summary>
        /// Будет контролировать процесс парсинга.
        /// </summary>
        public async void Worker()
        {
            for (int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++)
            {
                if (IsActive)//Проверка если запущин еще парсер.
                {
                    string source = await loader.GetSourceByPage(i); //Получаем код страницы.
                    HtmlParser domParser = new HtmlParser();//Парсер из AngleSharp.
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);//получить код с которым можно работать.
                    T result = parser.Parse(document);
                    items = result;
                    OnNewData?.Invoke(this, result);
                }
            }

            OnComplited?.Invoke(this);
            isActive = false;
        }
    }
}
