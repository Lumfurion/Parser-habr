using System.Collections.Generic;
using System.Xml.Serialization;
namespace Parser_habr.Models
{
    [XmlRoot(ElementName = "articles")]
    public class Articles
    {
        [XmlElement(ElementName = "article")]
        public List<Article> Articleslist { get; set; }
        public Articles() { }

        public static Articles Craete(List<Article> articles)
        {
            Articles art = new Articles();
            art.Articleslist = articles;
            return art;
        }
    }
}
