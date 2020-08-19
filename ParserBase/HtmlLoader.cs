using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParserBase
{
    /// <summary>
    ///Предназначение этого класса загружать код HTML страницы из указанных настроек парсера. 
    /// </summary>
    class HtmlLoader
    {   /// <summary>
        ///для отправки HTTP запросов и получения HTTP ответов. 
        /// </summary>
        readonly HttpClient client;
        /// <summary>
        /// будем передовать адрес
        /// </summary>
        readonly string url; 

        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# App"); //Это для индентификации на сайте-жертве.
            url = $"{settings.BaseUrl}/{settings.Postfix}/"; //Здесь собирается адресная строка.
        }
        /// <summary>
        /// Будем получать страницу по указанному id,например страница номер 1.
        /// </summary>
        /// <param name="id">id страницы</param>
        /// <returns></returns>
        public async Task<string> GetSourceByPage(int id) 
        {
            string currentUrl = url.Replace("{CurrentId}", id.ToString());//Подменяем {CurrentId} на номер страницы
            HttpResponseMessage responce = await client.GetAsync(currentUrl); //Получаем ответ с сайта.
            string source = default;

            if (responce != null && responce.StatusCode == HttpStatusCode.OK)//Проверка ответа от сайта.
            {
                source = await responce.Content.ReadAsStringAsync(); //Помещаем код страницы в переменную.
            }
            return source;
        }
    }
}
