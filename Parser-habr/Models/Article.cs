using System.Xml.Serialization;
namespace Parser_habr.Models
{
    [XmlRoot(ElementName = "article")]
    public class Article
    {
        [XmlIgnore]
        public int Id { set; get; }
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "text")]
        public string Text { get; set; }
        [XmlElement(ElementName = "image")]
        public string Image { get; set; }

        public Article() { }
        public Article(string title, string text, string image)
        {
            Title = title;
            Text = text;
            Image = image;
        }
    }
}
